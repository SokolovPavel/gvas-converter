# Unreal Engine 4 save game converter
This library and simple console tool for it will convert the generic UE4 save game file into a json for easier analysis.

Bakc convertion is theoretically possible, but is not implemented.

Due to limitations of how UE4 serializes the data, some data types might be missing, and might fail deserialization for some games.
For example, I know for a fact that there's at least a Set collection type, and a lot of less-frequently used primitive types (non-4 byte ints, double, etc).


# Astroneer savefile format reference
## Top-level structure
Astroneer savefile is a zip file.
Unzipped file is a gvas format file 
- Header (4bytes). Expected value: "GVAS"
- Version info
    - Savegame version (int32)
    - Package version (int32)
    - Engine version
        - Major (int16)
        - Minor (int16)
        - Patch (int16)
        - Build (int32)
    - Custom format version (int32)
- Custom format UUID array
    - Array size (int32)
    - Array of pairs UUID (16bytes) and Value (int32)
- Savegame type ([string]). Expected value: "AstroSave"
- None string ([string]) [Unknown]
- zero ([int32])
- magic number ([int32]) [Unknown]
- magic string ([string]) [Unknown]
- [String pool] String array referenced from component and properties type and names 
- [Instance pool] Component array 
- [Chunk pool] Chunk Array [Unknown]
- [Index pool] [Unknown]
- Rest bytes (10-20 bytes)

# String pool
Contains array of [string]. Referenced from all components and properties as name and type values. 
0 index reserved for null value
## Structure
- magic integer [int32]. Value: 44
- array ***size*** [int64]
- [string] array ***size*** length

# Components array
Contains mostly all in-game entities.

## Structure
- array ***size***
- [component] array ***size*** length

## Component
Structure
- Component class full name ([string])
- Component name ([string reference])
- Flag array
    - byte 1  - unknown
    - byte 2 - unknown
    - byte 3 - unknown
    - byte 4 - unknown
    - byte 5 - partially unknown
        - bit 0 - unknown
        - bit 1 - unknown
        - bit 2 - extended header flag
        - bit 3 - unknown
        - bit 4 - unknown
        - bit 5 - unknown
        - bit 6 - unknown
        - bit 7 - unknown
- Parent index ([int32]). [Component array] index
- (Optional) Extended header. Enabled by flag in Flag array
    - byte 1
    - byte 2
    - byte 3
    - byte 4
- Data size ([int32])
- Property array ended by [none-property]. Contains repeatable blocks of below structure:
    - Property name ([string-reference])
    - Property type ([string-reference])
    - Property length ([int64])
    - Property value ([property])
- Possible custom properties array?

## Property
### Simple properties.
Contains: 
- BoolProperty 
- IntProperty
- UInt32Property
- UInt64Property
- FloatProperty
- ByteProperty

Structure:
- Terminator ([byte]). 0 value expected
- Value. [byte] for Bool, 4 bytes for Int, Float, UInt32, Byte. 8 bytes for UInt64

### EnumProperty
Structure:
- EnumType ([string-reference])
- [Terminator]
- Value ([string-reference])

### NameProperty
Structure:
- [Terminator]
- Value ([string-reference])

### TextProperty
Structure:
- zero byte
- zero byte
- flags 4 bytes
- Name ([string-reference])
- Value ([string-reference])

### ArrayPropery
Structure:
- Item type ([string-reference])
- [Terminator]
- array ***size***
- Properties array

## Properties in arrays

## ArrayProperty of StructProperty
- Type ([string-reference])
- GUID. Empty guid expected
- [Terminator]
- Struct Values array
    -  

### StructProperty
 Contains Properies array
## String
Property format
string ***length*** (int32)
byte array ***length*** long. Utf8 encoding.

## String reference
[int32] value. Index in [String pool]
## int32
signed integer 4 byte long

## Unknown

[string]: #string
[int32]: #int32
[int64]: #int64
[property]: #property
[Unknown]: #Unknownpusposevalue
[string-reference]: #string-reference
[Terminator]: #terminator
