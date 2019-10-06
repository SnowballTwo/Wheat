using System.IO;
using NUnit.Framework;
using Wist.IO;

namespace Wheat.IO.Test
{
    [TestFixture]
    public class BitWriterTest
    {
        [Test]
        public void WriteBit()
        {
            var data = new byte[] { 0b11000000, 0b00001111, 0b11111100 };

            using ( var stream = new MemoryStream() )
            using ( var writer = new BitWriter( stream ) )
            {
                for ( var i = 0; i < 24; i++ )
                    writer.WriteBit( (byte) ( i / 6 % 2 ) );

                Assert.That( stream.ToArray(), Is.EquivalentTo( data ) );
            }
        }

        [Test]
        public void WriteBytes()
        {
            var data = new byte[] { 0b11000000, 0b00001111, 0b11111100 };

            using ( var stream = new MemoryStream() )
            using ( var writer = new BitWriter( stream ) )
            {
                foreach ( var b in data )
                    writer.Write( b );

                Assert.That( stream.ToArray(), Is.EquivalentTo( data ) );
            }
        }

        [Test]
        public void WriteInt16()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (short) -2000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt16(), Is.EqualTo( (short) -2000 ) );
                }
            }
        }

        [Test]
        public void WriteInt32()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( -8000000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt32(), Is.EqualTo( -8000000 ) );
                }
            }
        }

        [Test]
        public void WriteInt64()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( -30000000000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt64(), Is.EqualTo( -30000000000 ) );
                }
            }
        }

        [Test]
        public void WriteInt8()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (sbyte) -50 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt8(), Is.EqualTo( (sbyte) -50 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedInt16()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (short) -2000, 12 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt16( 12 ), Is.EqualTo( (short) -2000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedInt32()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( -8000000, 24 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt32( 24 ), Is.EqualTo( -8000000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedInt64()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( -30000000000, 36 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt64( 36 ), Is.EqualTo( -30000000000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedInt8()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (sbyte) -50, 7 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadInt8( 7 ), Is.EqualTo( (sbyte) -50 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedUInt16()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (ushort) 2000, 12 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt16( 12 ), Is.EqualTo( 2000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedUInt32()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (uint) 8000000, 24 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt32( 24 ), Is.EqualTo( 8000000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedUInt64()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (ulong) 30000000000, 36 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt64( 36 ), Is.EqualTo( 30000000000 ) );
                }
            }
        }

        [Test]
        public void WriteShortenedUInt8()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (byte) 100, 7 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt8( 7 ), Is.EqualTo( 100 ) );
                }
            }
        }

        [Test]
        public void WriteUInt16()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (ushort) 2000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt16(), Is.EqualTo( 2000 ) );
                }
            }
        }

        [Test]
        public void WriteUInt32()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (uint) 8000000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt32(), Is.EqualTo( 8000000 ) );
                }
            }
        }

        [Test]
        public void WriteUInt64()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (ulong) 30000000000 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt64(), Is.EqualTo( 30000000000 ) );
                }
            }
        }

        [Test]
        public void WriteUInt8()
        {
            using ( var stream = new MemoryStream() )
            {
                using ( var writer = new BitWriter( stream, true ) )
                {
                    writer.Write( (byte) 100 );
                }

                stream.Seek( 0, SeekOrigin.Begin );

                using ( var reader = new BitReader( stream ) )
                {
                    Assert.That( reader.ReadUInt8(), Is.EqualTo( 100 ) );
                }
            }
        }
    }
}