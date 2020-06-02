using System;
using static SDL2.SDL;

// c# port of https://wiki.libsdl.org/SDL_RenderDrawLine example

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (SDL_Init(SDL_INIT_VIDEO) == 0)
            {
                IntPtr window;
                IntPtr renderer;

                if (SDL_CreateWindowAndRenderer(640, 480, 0, out window, out renderer) == 0)
                {
                    var done = false;

                    while (!done)
                    {
                        SDL_Event e;

                        SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
                        SDL_RenderClear(renderer);

                        SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
                        SDL_RenderDrawLine(renderer, 320, 200, 300, 240);
                        SDL_RenderDrawLine(renderer, 300, 240, 340, 240);
                        SDL_RenderDrawLine(renderer, 340, 240, 320, 200);
                        SDL_RenderPresent(renderer);

                        while (SDL_PollEvent(out e) != 0)
                        {
                            if (e.type == SDL_EventType.SDL_QUIT)
                            {
                                done = true;
                            }
                        }
                    }
                }

                if (renderer != (IntPtr)0)
                {
                    SDL_DestroyRenderer(renderer);
                }
                if (window != (IntPtr)0)
                {
                    SDL_DestroyWindow(window);
                }
            }
            SDL_Quit();
        }
    }
}
