using System;
using System.IO;

namespace Wheat.IO
{
    /// <summary>
    ///     Provides methods to read bitwise from a stream or to read integers with arbitrary bit length.
    /// </summary>
    public sealed class BitReader : IDisposable
    {
        private readonly bool _LeaveOpen;
        private Stream _BaseStream;
        private byte _CurrentBit;
        private byte _CurrentByte;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitReader" /> class.
        /// </summary>
        /// <param name="input">The underlying input stream.</param>
        /// <param name="leaveOpen">
        ///     Indicates, whether the underlying stream should be closed when the writer is closed or
        ///     disposed.
        /// </param>
        /// <exception cref="NotSupportedException">Big endian systems are not supported by BitReader.</exception>
        public BitReader( Stream input, bool leaveOpen = false )
        {
            if ( !BitConverter.IsLittleEndian )
                throw new NotSupportedException( $"Big endian systems are not supported by {nameof( BitReader )}" );

            _BaseStream = input ?? throw new ArgumentNullException( nameof( input ) );
            _LeaveOpen = leaveOpen;
        }

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose( true );
        }

        #endregion

        /// <summary>
        ///     Reads a single bit from the underlying stream.
        /// </summary>
        /// <returns>0 or 1</returns>
        public byte ReadBit()
        {
            if ( _CurrentBit == 0 )
            {
                var next = _BaseStream.ReadByte();
                if ( next == -1 )
                    throw new EndOfStreamException();

                _CurrentByte = (byte) next;
            }

            var result = ( _CurrentByte & ( 1 << _CurrentBit ) ) >> _CurrentBit;

            _CurrentBit++;

            if ( _CurrentBit == 8 )
                _CurrentBit = 0;

            return (byte) result;
        }

        private byte[] ReadBits( int valueBitCount, int paddingBitCount = 0, bool signed = false )
        {
            var bitCount = Math.Max( valueBitCount, paddingBitCount );
            var bytes = (byte) Math.Ceiling( bitCount / 8.0 );
            var result = new byte[bytes];

            byte resultByte = 0;
            byte resultByteIndex = 0;
            byte resultBitIndex = 0;
            byte bit = 0;

            for ( var bitIndex = 0; bitIndex < bitCount; bitIndex++ )
            {
                if ( bitIndex < valueBitCount )
                    bit = ReadBit();
                else if ( !signed )
                    bit = 0;

                resultByte = (byte) ( resultByte | ( bit << resultBitIndex ) );
                resultBitIndex++;

                if ( resultBitIndex != 8 && bitIndex != bitCount - 1 )
                    continue;

                result[ resultByteIndex ] = resultByte;
                resultBitIndex = 0;
                resultByteIndex++;
                resultByte = 0;
            }

            return result;
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as an <see cref="sbyte" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="sbyte" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of an <see cref="sbyte" />
        /// </exception>
        public sbyte ReadInt8( int bits = 8 )
        {
            if ( bits > 8 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 16, true );

            return (sbyte) BitConverter.ToInt16( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as a <see cref="byte" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="byte" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of a <see cref="byte" />
        /// </exception>
        public byte ReadUInt8( int bits = 8 )
        {
            if ( bits > 8 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 16 );
            return (byte) BitConverter.ToUInt16( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as a <see cref="short" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="short" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of a <see cref="short" />
        /// </exception>
        public short ReadInt16( int bits = 16 )
        {
            if ( bits > 16 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 16, true );

            return BitConverter.ToInt16( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as an <see cref="ushort" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="ushort" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of an <see cref="ushort" />
        /// </exception>
        public ushort ReadUInt16( int bits = 16 )
        {
            if ( bits > 16 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 16 );
            return BitConverter.ToUInt16( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as an <see cref="int" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="int" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of an <see cref="int" />
        /// </exception>
        public int ReadInt32( int bits = 32 )
        {
            if ( bits > 32 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 32, true );
            return BitConverter.ToInt32( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as an <see cref="uint" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="uint" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of an <see cref="uint" />
        /// </exception>
        public uint ReadUInt32( int bits = 32 )
        {
            if ( bits > 32 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 32 );
            return BitConverter.ToUInt32( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as a <see cref="long" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="long" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of a <see cref="long" />
        /// </exception>
        public long ReadInt64( int bits = 64 )
        {
            if ( bits > 64 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 64, true );
            return BitConverter.ToInt64( bytes, 0 );
        }

        /// <summary>
        ///     Reads the specified number of <paramref name="bits" /> from the underlying stream and returns them
        ///     as an <see cref="ulong" />.
        /// </summary>
        /// <param name="bits">The number of bits that should be read from the underlying stream.</param>
        /// <returns>An <see cref="ulong" /> value, representing the bits.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The specified number of bits must be between 0 and the
        ///     size of an <see cref="ulong" />
        /// </exception>
        public ulong ReadUInt64( int bits = 64 )
        {
            if ( bits > 64 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            var bytes = ReadBits( bits, 64 );
            return BitConverter.ToUInt64( bytes, 0 );
        }

        private void Dispose( bool disposing )
        {
            if ( !disposing )
                return;

            var copyOfStream = _BaseStream;
            _BaseStream = null;
            if ( copyOfStream != null && !_LeaveOpen )
                copyOfStream.Dispose();
        }
    }
}