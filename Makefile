# Makefile for SDL2#
# Written by Ethan "flibitijibibo" Lee

# Source Lists
SRC = \
	src/LPUtf8StrMarshaler.cs \
	src/SDL2.cs \
	src/SDL2_image.cs \
	src/SDL2_mixer.cs \
	src/SDL2_ttf.cs

# Targets

debug: clean-debug
	mkdir -p bin/debug
	cp SDL2-CS.dll.config bin/debug
	dmcs /unsafe -debug -out:bin/debug/SDL2-CS.dll -target:library $(SRC)

clean-debug:
	rm -rf bin/debug

release: clean-release
	mkdir -p bin/release
	cp SDL2-CS.dll.config bin/release
	dmcs /unsafe -optimize -out:bin/release/SDL2-CS.dll -target:library $(SRC)

clean-release:
	rm -rf bin/release

clean: clean-debug clean-release
	rm -rf bin

all: debug release
