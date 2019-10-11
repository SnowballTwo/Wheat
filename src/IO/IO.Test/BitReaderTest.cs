using System;
using System.IO;
using NUnit.Framework;

namespace Wheat.IO.Test
{
    public class BitReaderTest
    {
        [Test]
        public void ReadBits()
        {
            var data = new byte[] { 0b11000000, 0b00001111, 0b11111100 };

            using ( var stream = new MemoryStream( data ) )
            using ( var reader = new BitReader( stream ) )
            {
                for ( var i = 0; i < 24; i++ )
                    Assert.That( reader.ReadBit(), Is.EqualTo( i / 6 % 2 ), $"Index {i}" );
            }
        }

        [Test]
        public void ReadBytes()
        {
            var data = new byte[] { 0b00000011, 0b11110000, 0b00111111 };

            using ( var stream = new MemoryStream( data ) )
            using ( var reader = new BitReader( stream ) )
            {
                foreach ( var t in data )
                    Assert.That( reader.ReadUInt8(), Is.EqualTo( t ) );
            }
        }

        [Test]
        public void ReadShortenedInt8()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (sbyte) -50 ) ) ) )
            {
                Assert.That( reader.ReadInt8( 7 ), Is.EqualTo( (sbyte) -50 ) );
            }
        }

        [Test]
        public void ReadShortenedUInt8()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (byte) 100 ) ) ) )
            {
                Assert.That( reader.ReadUInt8( 7 ), Is.EqualTo( (byte) 100 ) );
            }
        }

        [Test]
        public void ReadShortenedInt16()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (short) -1234 ) ) ) )
            {
                Assert.That( reader.ReadInt16( 12 ), Is.EqualTo( (short) -1234 ) );
            }
        }

        [Test]
        public void ReadShortenedUInt16()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (ushort) 1234 ) ) ) )
            {
                Assert.That( reader.ReadUInt16( 12 ), Is.EqualTo( (ushort) 1234 ) );
            }
        }

        [Test]
        public void ReadShortenedInt32()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( -6543210 ) ) ) )
            {
                Assert.That( reader.ReadInt32( 24 ), Is.EqualTo( -6543210 ) );
            }
        }

        [Test]
        public void ReadShortenedUInt32()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (uint) 6543210 ) ) ) )
            {
                Assert.That( reader.ReadUInt32( 24 ), Is.EqualTo( (uint) 6543210 ) );
            }
        }

        [Test]
        public void ReadShortenedInt64()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( -32150123456 ) ) ) )
            {
                Assert.That( reader.ReadInt64( 36 ), Is.EqualTo( -32150123456 ) );
            }
        }

        [Test]
        public void ReadShortenedUInt64()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (ulong) 32150123456 ) ) ) )
            {
                Assert.That( reader.ReadUInt64( 36 ), Is.EqualTo( (ulong) 32150123456 ) );
            }
        }

        [Test]
        public void ReadInt8()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (sbyte) -50 ) ) ) )
            {
                Assert.That( reader.ReadInt8(), Is.EqualTo( (sbyte) -50 ) );
            }
        }

        [Test]
        public void ReadUInt8()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (byte) 100 ) ) ) )
            {
                Assert.That( reader.ReadUInt8(), Is.EqualTo( (byte) 100 ) );
            }
        }

        [Test]
        public void ReadInt16()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (short) -1234 ) ) ) )
            {
                Assert.That( reader.ReadInt16(), Is.EqualTo( (short) -1234 ) );
            }
        }

        [Test]
        public void ReadUInt16()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (ushort) 1234 ) ) ) )
            {
                Assert.That( reader.ReadUInt16(), Is.EqualTo( (ushort) 1234 ) );
            }
        }

        [Test]
        public void ReadInt32()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( -6543210 ) ) ) )
            {
                Assert.That( reader.ReadInt32(), Is.EqualTo( -6543210 ) );
            }
        }

        [Test]
        public void ReadUInt32()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (uint) 6543210 ) ) ) )
            {
                Assert.That( reader.ReadUInt32(), Is.EqualTo( (uint) 6543210 ) );
            }
        }

        [Test]
        public void ReadInt64()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( -32150123456 ) ) ) )
            {
                Assert.That( reader.ReadInt64(), Is.EqualTo( -32150123456 ) );
            }
        }

        [Test]
        public void ReadUInt64()
        {
            using ( var reader = new BitReader( new MemoryStream( BitConverter.GetBytes( (ulong) 32150123456 ) ) ) )
            {
                Assert.That( reader.ReadUInt64(), Is.EqualTo( (ulong) 32150123456 ) );
            }
        }
    }
}