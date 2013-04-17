# Makefile for SDL2#
# Written by Ethan "flibitijibibo" Lee

# Source Lists
SDL2 = \
	src/SDL2.cs \
	src/SDL2_image.cs \
	src/SDL2_mixer.cs \
	src/SDL2_ttf.cs

# Targets

build:
	mkdir bin
	cp SDL2#.dll.config bin
	dmcs /unsafe -debug -out:bin/SDL2#.dll -target:library $(SDL2)

clean:
	rm -rf bin
