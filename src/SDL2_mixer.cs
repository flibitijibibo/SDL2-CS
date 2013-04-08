#region License
/* SDL2# - C# Wrapper for SDL2
 *
 * Copyright (c) 2013 Ethan Lee.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */
#endregion

#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

namespace SDL2
{
	public static class SDL2_mixer
	{
		#region SDL2# Variables
		
		/* Used by DllImport to load the native library. */
		internal const String nativeLibName = "SDL2_mixer.dll";
		
		#endregion
		
		#region SDL_mixer.h
		
		// FIXME: Allow user defined MIX_CHANNELS
		public const int MIX_CHANNELS = 8;
		
		public const int MIX_DEFAULT_FREQUENCY = 22050;
		// FIXME: ASSUMING LITTLE ENDIAN!!! Big endian: SDL2.AUDIO_S16MSB
		public const ushort MIX_DEFAULT_FORMAT = SDL2.AUDIO_S16LSB;
		public const int MIX_DEFAULT_CHANNELS = 2;
		public const byte MIX_MAX_VOLUME = 128;
		
		[Flags]
		public enum MIX_InitFlags
		{
			MIX_INIT_FLAC =		0x00000001,
			MIX_INIT_MOD =		0x00000002,
			MIX_INIT_MP3 =		0x00000004,
			MIX_INIT_OGG =		0x00000008,
			MIX_INIT_FLUIDSYNTH =	0x00000010,
		}
		
		public enum Mix_Fading
		{
			MIX_NO_FADING,
			MIX_FADING_OUT,
			MIX_FADING_IN
		}
		
		public enum Mix_MusicType
		{
			MUS_NONE,
			MUS_CMD,
			MUS_WAV,
			MUS_MOD,
			MUS_MID,
			MUS_OGG,
			MUS_MP3,
			MUS_MP3_MAD,
			MUS_FLAC,
			MUS_MODPLUG
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct Mix_Chunk
		{
			public int allocated;
			public IntPtr abuf; // Uint8*
			public uint alen;
			public byte volume;
		}
		
		public delegate void MixFuncDelegate(
			IntPtr udata, // void*
			IntPtr stream, // Uint8*
			int len
		);
		
		public delegate void MusicFinishedDelegate();
		
		public delegate void ChannelFinishedDelegate(int channel);
		
		public delegate int SoundFontDelegate(
			IntPtr a, // const char*
			IntPtr b // void*
		);
		
		[DllImport(nativeLibName, EntryPoint = "MIX_Linked_Version")]
		private static extern IntPtr INTERNAL_MIX_Linked_Version();
		public static SDL2.SDL_version MIX_Linked_Version()
		{
			SDL2.SDL_version result;
			IntPtr result_ptr = INTERNAL_MIX_Linked_Version();
			result = (SDL2.SDL_version) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName)]
		public static extern int Mix_Init(MIX_InitFlags flags);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_Quit();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_OpenAudio(
			int frequency,
			ushort format,
			int channels,
			int chunksize
		);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_AllocateChannels(int numchans);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_QuerySpec(
			ref int frequency,
			ref ushort format,
			ref int channels
		);
		
		/* These are for Mix_LoadWAV, which is a macro in the C header.
		 * THIS IS AN RWops FUNCTION!
		 */
		[DllImport(nativeLibName, EntryPoint = "Mix_LoadWAV_RW")]
		private static extern IntPtr INTERNAL_Mix_LoadWAV_RW(
			IntPtr src,
			int freesrc
		);
		public static Mix_Chunk Mix_LoadWAV(string file)
		{
			Mix_Chunk result;
			IntPtr rwops = SDL2.INTERNAL_SDL_RWFromFile(file, "rb");
			IntPtr result_ptr = INTERNAL_Mix_LoadWAV_RW(rwops, 1);
			result = (Mix_Chunk) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		/* IntPtr refers to a Mix_Music* */
		[DllImport(nativeLibName)]
		public static extern IntPtr Mix_LoadMUS(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string file
		);
		
		[DllImport(nativeLibName, EntryPoint = "Mix_QuickLoad_WAV")]
		private static extern IntPtr INTERNAL_Mix_QuickLoad_WAV(byte[] mem);
		public static Mix_Chunk Mix_QuickLoad_WAV(byte[] mem)
		{
			Mix_Chunk result;
			IntPtr result_ptr = INTERNAL_Mix_QuickLoad_WAV(mem);
			result = (Mix_Chunk) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName, EntryPoint = "Mix_QuickLoad_RAW")]
		private static extern IntPtr INTERNAL_Mix_QuickLoad_RAW(
			byte[] mem,
			uint len
		);
		public static Mix_Chunk Mix_QuickLoad_RAW(byte[] mem, uint len)
		{
			Mix_Chunk result;
			IntPtr result_ptr = INTERNAL_Mix_QuickLoad_RAW(mem, len);
			result = (Mix_Chunk) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName)]
		public static extern void Mix_FreeChunk(ref Mix_Chunk chunk);
		
		/* music refers to a Mix_Music* */
		[DllImport(nativeLibName)]
		public static extern void Mix_FreeMusic(IntPtr music);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GetNumChunkDecoders();
		
		[DllImport(nativeLibName, EntryPoint = "Mix_GetChunkDecoder")]
		private static extern IntPtr INTERNAL_Mix_GetChunkDecoder(int index);
		public static string Mix_GetChunkDecoder(int index)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_Mix_GetChunkDecoder(index)
			);
		}
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GetNumMusicDecoders();
		
		[DllImport(nativeLibName, EntryPoint = "Mix_GetMusicDecoder")]
		private static extern IntPtr INTERNAL_Mix_GetMusicDecoder(int index);
		public static string Mix_GetMusicDecoder(int index)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_Mix_GetMusicDecoder(index)
			);
		}
		
		/* music refers to a Mix_Music* */
		[DllImport(nativeLibName)]
		public static extern Mix_MusicType Mix_GetMusicType(IntPtr music);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_SetPostMix(
			MixFuncDelegate mix_func,
			IntPtr arg // void*
		);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_HookMusic(
			MixFuncDelegate mix_func,
			IntPtr arg // void*
		);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_HookMusicFinished(
			MusicFinishedDelegate music_finished
		);
		
		/* IntPtr refers to a void* */
		[DllImport(nativeLibName)]
		public static extern IntPtr Mix_GetMusicHookData();
		
		[DllImport(nativeLibName)]
		public static extern void Mix_ChannelFinished(
			ChannelFinishedDelegate channel_finished
		);
		
		/* TODO: Special Effects API.
		 * As of 05d12cea0253 (HEAD on April 7, 2013), lines 231 - 497.
		 */
		
		[DllImport(nativeLibName)]
		public static extern int Mix_ReserveChannels(int num);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupChannel(int which, int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupChannels(int from, int to, int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupAvailable(int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupCount(int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupOldest(int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GroupNewer(int tag);
		
		public static int Mix_PlayChannel(
			int channel,
			ref Mix_Chunk chunk,
			int loops
		) {
			return Mix_PlayChannelTimed(channel, ref chunk, loops, -1);
		}
		
		[DllImport(nativeLibName)]
		public static extern int Mix_PlayChannelTimed(
			int channel,
			ref Mix_Chunk chunk,
			int loops,
			int ticks
		);
		
		/* music refers to a Mix_Music* */
		[DllImport(nativeLibName)]
		public static extern int Mix_PlayMusic(IntPtr music, int loops);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeInMusic(
			IntPtr music,
			int loops,
			int ms
		);
		
		/* music refers to a Mix_Music* */
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeInMusicPos(
			IntPtr music,
			int loops,
			int ms,
			double position
		);
		
		public static int Mix_FadeInChannel(
			int channel,
			ref Mix_Chunk chunk,
			int loops,
			int ms
		) {
			return Mix_FadeInChannelTimed(channel, ref chunk, loops, ms, -1);
		}
		
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeInChannelTimed(
			int channel,
			ref Mix_Chunk chunk,
			int loops,
			int ms,
			int ticks
		);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_Volume(int channel, int volume);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_VolumeChunk(
			ref Mix_Chunk chunk,
			int volume
		);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_VolumeMusic(int volume);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_HaltChannel(int channel);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_HaltGroup(int tag);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_HaltMusic();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_ExpireChannel(int channel, int ticks);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeOutChannel(int which, int ms);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeOutGroup(int tag, int ms);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_FadeOutMusic(int ms);
		
		[DllImport(nativeLibName)]
		public static extern Mix_Fading Mix_FadingMusic();
		
		[DllImport(nativeLibName)]
		public static extern Mix_Fading Mix_FadingChannel(int which);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_Pause(int channel);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_Resume(int channel);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_Paused(int channel);
		
		[DllImport(nativeLibName)]
		public static extern void Mix_PauseMusic();
		
		[DllImport(nativeLibName)]
		public static extern void Mix_ResumeMusic();
		
		[DllImport(nativeLibName)]
		public static extern void Mix_RewindMusic();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_PausedMusic();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_SetMusicPosition(double position);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_Playing(int channel);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_PlayingMusic();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_SetMusicCMD(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string command
		);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_SetSynchroValue(int value);
		
		[DllImport(nativeLibName)]
		public static extern int Mix_GetSynchroValue();
		
		[DllImport(nativeLibName)]
		public static extern int Mix_SetSoundFonts(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string paths
		);
		
		[DllImport(nativeLibName, EntryPoint = "Mix_GetSoundFonts")]
		private static extern IntPtr INTERNAL_Mix_GetSoundFonts();
		public static string Mix_GetSoundFonts()
		{
			return Marshal.PtrToStringAnsi(INTERNAL_Mix_GetSoundFonts());
		}
		
		[DllImport(nativeLibName)]
		public static extern int Mix_EachSoundFont(
			SoundFontDelegate function,
			IntPtr data // void*
		);
		
		[DllImport(nativeLibName, EntryPoint = "Mix_GetChunk")]
		private static extern IntPtr INTERNAL_Mix_GetChunk(int channel);
		public static Mix_Chunk Mix_GetChunk(int channel)
		{
			Mix_Chunk result;
			IntPtr result_ptr = INTERNAL_Mix_GetChunk(channel);
			result = (Mix_Chunk) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName)]
		public static extern void Mix_CloseAudio();
		
		#endregion
	}
}