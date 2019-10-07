

# ![alt text](https://github.com/SnowballTwo/Wheat/blob/master/wheat32.png "What icon") Wheat

SnowballTwo's field of useful tools and libraries.

## Wheat.IO

### `BitReader`

```csharp
var data = new byte[] { 0b11000000, 0b00001111, 0b11111100 };

using ( var stream = new MemoryStream( data ) )
using ( var reader = new BitReader( stream ) )
{
    for ( var i = 0; i < 24; i++ )
       Console.Write( reader.ReadBit() );
}
```

### BitWriter

```csharp
using ( var stream = new MemoryStream() )
using ( var writer = new BitWriter( stream ) )
{
    for ( var i = 0; i < 24; i++ )
        writer.WriteBit( (byte) ( i / 6 % 2 ) );

    Console.WriteLine( stream.ToArray() );
}
```


