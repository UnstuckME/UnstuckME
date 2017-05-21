using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;

namespace UnstuckME_Classes
{
    [Serializable]
    [ComVisible(true)]
    public class UnstuckMEStream : Stream
    {
        private byte[] _buffer;     //Either allocated internally or externally
        private readonly int _origin;        //For user-provided arrays, start at this origin
        private int _position;      //read/write head
        [ContractPublicPropertyName("Length")]
        private int _length;        //Number of bytes within the stream
        private int _capactiy;      //length of usable portion of buffer for stream (note that _capacity == _buffer.Length for non-user-provided byte[]'s)
        private bool _expandable;   //User-provided buffers aren't expandable
        private bool _writable;     //Can user write to this stream?
        private bool _isOpen;       //Is this stream open or closed?
        private const int MemStreamMaxLength = int.MaxValue;
        private int _userID;     //The UnstuckME user information used for processing the stream on the server
        private string _filename;    //The name of the file
        private string _path;        //The full path where the file is located

        /// <summary>
        /// Gets/sets the User ID of the user who the file belongs to
        /// </summary>
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        /// <summary>
        /// Gets/sets the name of the file
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// Gets/sets the full path where the file is located locally
        /// </summary>
        public string FilePath
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// A constructor for an UnstuckMEStream that initializes the byte stream with a maximum capacity.
        /// Because byte stream is not provided by user, it is expandable.
        /// </summary>
        /// <param name="capacity">The maximum capacity for the byte stream.</param>
        public UnstuckMEStream(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Argument Out of Range, Negative Capacity");

            Contract.EndContractBlock();

            _buffer = new byte[capacity];
            _capactiy = capacity;
            _expandable = true;
            _writable = true;
            _origin = 0;        //Must be 0 for byte[]'s created by UnstuckMEStream
            _isOpen = true;
        }

        /// <summary>
        /// A constructor for an UnstuckMEStream that allows a user to provide a btye stream and determine if
        /// the stream is writeable or not (the Seek() and Write() functions depend on this). Because the byte
        /// stream is provided by the user, it is not expandable.
        /// </summary>
        /// <param name="buffer">The byte stream provided by the user.</param>
        /// <param name="writable">Determines whether the stream can be edited or overwritten.</param>
        public UnstuckMEStream(byte[] buffer, bool writable)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Null Buffer");
            Contract.EndContractBlock();

            _buffer = buffer;
            _length = _capactiy = buffer.Length;
            _writable = writable;
            _origin = 0;
            _isOpen = true;
        }

        /// <summary>
        /// Gets a value determining if the stream can be read.
        /// </summary>
        public override bool CanRead
        {
            [Pure]
            get { return _isOpen; }
        }

        /// <summary>
        /// Gets a value determining if seeking through the stream is possible.
        /// If false, Position, Seek, Langth, and SetLength should throw.
        /// </summary>
        public override bool CanSeek
        {
            [Pure]
            get { return _isOpen; }
        }

        /// <summary>
        /// Gets a value determining if the stream can be edited or overwritten.
        /// </summary>
        public override bool CanWrite
        {
            [Pure]
            get { return _writable; }
        }

        /// <summary>
        /// Dispose of the stream to avoid potential memory leaks, or put the stream inside a using statement.
        /// NOTE: Never change this to call other virtual methods on Stream like write, since the state on subclasses
        /// has already been torn down. This is the last code to run on cleanup for a stream.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    _isOpen = false;
                    _writable = false;
                    _expandable = false;
                    //Don't set buffer to null - allow TryGetBuffer, getBuffer, & ToArray to work
                }
            }
            finally
            {
                //call base.Close() to cleanup async IO resources
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Gets the length of the stream so long as the stream is open.
        /// </summary>
        public override long Length
        {
            get
            {
                if (!_isOpen)
                    throw new ObjectDisposedException("File Closed/Disposed");

                return _length - _origin;
            }
        }

        /// <summary>
        /// Adjusts the position of the stream where the read takes place.
        /// </summary>
        public override long Position
        {
            get
            {
                if (!_isOpen)
                    throw new ObjectDisposedException("File Closed/Disposed");

                return _position - _origin;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Argument Out of Range, Need Nonnegative Number");

                Contract.Ensures(Position == value);
                Contract.EndContractBlock();

                if (!_isOpen)
                    throw new ObjectDisposedException("File Closed/Disposed");
                if (value > MemStreamMaxLength)
                    throw new ArgumentOutOfRangeException(nameof(value), "Argument Out of Range, Need Nonnegative Number");

                _position = _origin + (int)value;
            }
        }

        /// <summary>
        /// Determines if the capacity of the stream can be changed to a new value and allocates a new byte stream with the new capacity.
        /// </summary>
        /// <param name="value">The new capacity of the stream.</param>
        /// <returns>A bool saying whether we allocated a new array.</returns>
        private bool EnsureCapacity(int value)
        {
            if (value < 0)  //check for overflow
                throw new IOException("Stream Too Long");
            if (value > _capactiy)
            {
                int newCapacity = value;
                if (newCapacity < 256)
                    newCapacity = 256;
                //we are ok with this overflowing since the next statement will deal with the cases where _capacity * 2 overflows
                if (newCapacity < _capactiy * 2)
                    newCapacity = _capactiy * 2;
                //We want to expand the array up to Array.MaxArrayLengthOneDimensional and we want to give the user the value that they asked for
                if ((uint)(_capactiy * 2) > 0x7FFFFFC7)
                    newCapacity = value > 0x7FFFFFC7 ? value : 0x7FFFFFC7;

                Capacity = newCapacity;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets and sets the capacity (number of bytes allocated) for this stream. The capacity cannot be set to a value less than the current length of the stream.
        /// </summary>
        public virtual int Capacity
        {
            get
            {
                if (!_isOpen)
                    throw new ObjectDisposedException("File Closed/Disposed");

                return _capactiy - _origin;
            }
            set
            {
                //Only update the capacity if the stream is expandable and the value is different than the current capacity.
                //Special behavior if the stream isn't expandable: we don't throw if value is the same as the current capacity.
                if (value < Length)
                    throw new ArgumentOutOfRangeException(nameof(value), "Argument Out of Range, Capacity Too Small");

                Contract.Ensures(_capactiy - _origin == value);
                Contract.EndContractBlock();

                if (!_isOpen)
                    throw new ObjectDisposedException("File Closed/Disposed");
                if (_expandable && value != _capactiy)  //MemoryStream has this invariant: _origin > 0 => !expandable (see ctors)
                {
                    if (value > 0)
                    {
                        byte[] newBuffer = new byte[value];

                        if (_length > 0)
                            Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _length);

                        _buffer = newBuffer;
                    }
                    else
                        _buffer = null;

                    _capactiy = value;
                }
            }
        }

        /// <summary>
        /// MemoryStream implementation apparently doesn't do anything with this, so neither will UnstuckMEStream.
        /// </summary>
        public override void Flush()
        { }

        /// <summary>
        /// Reads the specified amount of data from the stream into the buffer from the offset position.
        /// </summary>
        /// <param name="buffer">The buffer to write the data read from the stream.</param>
        /// <param name="offset">The position to begin reading from the stream.</param>
        /// <param name="count">The number of bytes to read from the stream.</param>
        /// <returns>The amount of bytes read from the stream.</returns>
        public override int Read([In, Out]byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Null Buffer");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Argument Out of Range, Need Nonnegative Number");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Argument Out of Range, Need Nonnegative Number");
            if (buffer.Length - offset < count)
                throw new ArgumentException("Offset Length Argument Invalid");

            Contract.EndContractBlock();

            if (!_isOpen)
                throw new ObjectDisposedException("File Closed/Disposed");

            int n = _length - _position;

            if (n > count)
                n = count;
            if (n <= 0)
                return 0;

            Contract.Assert(_position + n >= 0, "_position + n >= 0");

            if (n <= 8)
            {
                int byteCount = n;
                while (--byteCount >= 0)
                    buffer[offset + byteCount] = _buffer[_position + byteCount];
            }
            else
                Buffer.BlockCopy(_buffer, _position, buffer, offset, n);

            _position += n;
            return n;
        }

        /// <summary>
        /// Adjusts the position to begin reading from the Stream.
        /// </summary>
        /// <param name="offset">The amount to seek forward or backward from the position specified by <paramref name="origin"/>.</param>
        /// <param name="origin">Specifies the position in a stream to use for seeking. Can be from the beginning, the end, or the current position.</param>
        /// <returns>The new position in the stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!_isOpen)
                throw new ObjectDisposedException("File Closed/Disposed");
            if (offset > MemStreamMaxLength)
                throw new ArgumentOutOfRangeException(nameof(offset), "Stream Length Out of Range");

            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        int tempPos = unchecked(_origin + (int)offset);
                        if (offset < 0 || tempPos < _origin)
                            throw new IOException("Seeking Before Begin");

                        _position = tempPos;
                        break;
                    }
                case SeekOrigin.Current:
                    {
                        int tempPos = unchecked(_position + (int)offset);
                        if (unchecked(_position + offset) < _origin || tempPos < _origin)
                            throw new IOException("Seeking Before Begin");

                        _position = tempPos;
                        break;
                    }
                case SeekOrigin.End:
                    {
                        int tempPos = unchecked(_length + (int)offset);
                        if (unchecked(_length + offset) < _origin || tempPos < _origin)
                            throw new IOException("Seeking Before Begin");

                        _position = tempPos;
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid Seek Origin");
            }

            Contract.Assert(_position >= 0, "_position >= 0");
            return _position;
        }

        /// <summary>
        /// Sets the length of the stream to a given value. the new value must be nonnegative and less than the space remaining in the array, Int32.MaxValue - origin.
        /// Origin is 0 in all cases other than a UnstuckMEStream created on top of an existing array and a specific starting offset was passedinto the construtor.
        /// The upper bounds prevents any situations where a stream may be created on top of an array then the stream is made longer than the maximum possible length
        /// of the array (Int32.MaxValue).
        /// </summary>
        /// <param name="value">The new length of the stream.</param>
        public override void SetLength(long value)
        {
            if (value < 0 || value > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value), "Argument Out of Range, Stream Length");

            Contract.Ensures(_length - _origin == value);
            Contract.EndContractBlock();

            if (!CanWrite)
                throw new NotSupportedException("Not Supported, Unwritable Stream");

            Contract.Assert(MemStreamMaxLength == int.MaxValue);    //Origin wasn't publicly exposed above. Check parameter validation logic in this method of this fails

            if (value > int.MaxValue - _origin)
                throw new ArgumentOutOfRangeException(nameof(value), "Argument Out of Range, Stream Length");

            int newLength = _origin + (int)value;
            bool allocatedNewArray = EnsureCapacity(newLength);

            if (!allocatedNewArray && newLength > _length)
                Array.Clear(_buffer, _length, newLength - _length);
            if (_position > newLength)
                _position = newLength;
        }

        /// <summary>
        /// Reads the specified amount of bytes from the specified offset from the buffer and writes it to the stream.
        /// </summary>
        /// <param name="buffer">The byte stream to get the bytes to write to the UnstuckMEStream.</param>
        /// <param name="offset">The offset to begin reading from the byte stream.</param>
        /// <param name="count">The number of bytes to write from the byte stream to the UnstuckMEStream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Null Buffer");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Argument Out of Range, Need Nonnegative Number");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Argument Out of Range, Need Nonnegative Number");
            if (buffer.Length - offset < count)
                throw new ArgumentException("Invalid Offset Length");

            Contract.EndContractBlock();

            if (!_isOpen)
                throw new ObjectDisposedException("File Closed/Disposed");
            if (!CanWrite)
                throw new NotSupportedException("Not Supported, Unwritable Stream");

            int i = _position + count;
            if (i < 0)  //check for overflow
                throw new IOException("Stream Too Long");
            if (i > _length)
            {
                bool mustZero = _position > _length;
                if (i > _capactiy)
                {
                    bool allocatedArray = EnsureCapacity(i);
                    if (allocatedArray)
                        mustZero = false;
                }
                if (mustZero)
                    Array.Clear(_buffer, _length, i - _length);

                _length = i;
            }
            if (count <= 8 && buffer != _buffer)
            {
                int byteCount = count;
                while (--byteCount >= 0)
                    _buffer[_position + byteCount] = buffer[offset + byteCount];
            }
            else
                Buffer.BlockCopy(buffer, offset, _buffer, _position, count);

            _position = i;
        }
    }
}