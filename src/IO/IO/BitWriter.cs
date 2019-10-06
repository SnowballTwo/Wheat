using System;
using System.IO;

namespace Wheat.IO
{
    /// <summary>
    ///     Provides methods to write bitwise into a stream or to write integers with arbitrary bit length.
    /// </summary>
    public sealed class BitWriter : IDisposable
    {
        private readonly bool _LeaveOpen;
        private Stream _BaseStream;
        private byte _CurrentBit;
        private byte _CurrentByte;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitWriter" /> class.
        /// </summary>
        /// <param name="output">Underlying stream</param>
        /// <param name="leaveOpen">
        ///     Indicates, whether the underlying stream should be closed when the writer is closed or
        ///     disposed.
        /// </param>
        public BitWriter( Stream output, bool leaveOpen = false )
        {
            _BaseStream = output ?? throw new ArgumentNullException( nameof( output ) );
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
        ///     Writes the lowest bit of the specified <paramref name="value" /> into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the lowest bit is written.</param>
        public void WriteBit( byte value )
        {
            _CurrentByte = (byte) ( _CurrentByte | ( ( value & 0x01 ) << _CurrentBit ) );
            _CurrentBit++;

            if ( _CurrentBit != 8 )
                return;

            _BaseStream.WriteByte( _CurrentByte );

            _CurrentByte = 0;
            _CurrentBit = 0;
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( sbyte value, int bits = 8 )
        {
            if ( bits > 8 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( byte value, int bits = 8 )
        {
            if ( bits > 8 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            if ( _CurrentBit == 0 && bits == 8 )
                _BaseStream.WriteByte( value );
            else
                for ( var i = 0; i < bits; i++ )
                    WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( short value, int bits = 16 )
        {
            if ( bits > 16 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( ushort value, int bits = 16 )
        {
            if ( bits > 16 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( int value, int bits = 32 )
        {
            if ( bits > 32 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( uint value, int bits = 32 )
        {
            if ( bits > 32 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1 << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( long value, int bits = 64 )
        {
            if ( bits > 64 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1L << i ) ) >> i ) );
        }

        /// <summary>
        ///     Writes the specified number of <paramref name="bits" /> from the specified <paramref name="value" />
        ///     into the underlying stream.
        /// </summary>
        /// <param name="value">The value from which the bits are written.</param>
        /// <param name="bits">The number of bits that should be written.</param>
        public void Write( ulong value, int bits = 64 )
        {
            if ( bits > 64 || bits < 1 )
                throw new ArgumentOutOfRangeException( nameof( bits ) );

            for ( var i = 0; i < bits; i++ )
                WriteBit( (byte) ( ( value & ( 1UL << i ) ) >> i ) );
        }

        /// <summary>
        ///     Closes the writer and writes pending bits to the underlying base stream.
        /// </summary>
        public void Close()
        {
            Dispose( true );
        }

        private void Dispose( bool disposing )
        {
            if ( !disposing )
                return;

            if ( _CurrentBit != 0 && _BaseStream != null )
            {
                _BaseStream.WriteByte( _CurrentByte );
                _CurrentByte = 0;
                _CurrentBit = 0;
            }

            var copyOfStream = _BaseStream;
            _BaseStream = null;
            if ( copyOfStream != null && !_LeaveOpen )
                copyOfStream.Close();
        }
    }
}