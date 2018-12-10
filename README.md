# Any Base File Reader

## Summary:

Usually files are read in bytes (or 8 bits) at a time and are displayed as 
 ASCII characters or hexadecimals. This small utility reads files at any 
 number of bits at a time, and displays them at any base. For example, 
 this utility can read a simple text file 19 bits at a time, and display 
 the contents read, in base 8

## Usage:

Create a text file (say test.txt) with the content "This is a dummy text". 
 Execute the following commands, and check the output:

```
G:\anyBaseFileReader>anyBaseFileReader.exe some_text.txt 8 16
54             68             69             73             -              20             69             73             20
61             20             64             75             -              6d             6d             79             20
74             65             78             74             -

G:\anyBaseFileReader>anyBaseFileReader.exe some_text.txt 16 16
6854           7369           6920           2073           -              2061           7564           6d6d           2079
6574           7478

G:\anyBaseFileReader>anyBaseFileReader.exe some_text.txt 19 16
16854          40e6d          1cda4          3090           -              75642          2dada          5081e          3c32b
74
```

## Compilation:

Please execute the following commands:

```
C:\AnyBaseFileReader> compile.bat
  
```
