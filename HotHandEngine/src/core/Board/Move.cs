

//This struct hold the data for a move, This should be a 32 bit number with the following format

//000000 000000 0000 0000 0000 0 0 0 0000

/*
Bits | Meaning
------ + ------------------------------------------------
0–5   | From square(0–63)
6–11  | To square(0–63)
12–15 | Moving piece(0–15)
16–19 | Captured piece(0–15)
20–23 | Promotion piece(0–15)
24    | En passant flag
25    | Castling flag
26    | Double pawn push flag
27–31 | Reserved (score, ordering, etc.)
*/

namespace Chess.Core
{
    public readonly struct Move
    {
        public readonly uint bitMove; 

        public Move(int fromSquare, int toSquare, int movePiece, int capturedPiece, int promoPiece, bool epFlag, bool castle, bool dblPawn, int extra = 0)
        {
            bitMove =
                ((uint)fromSquare & 0x3F) |
                (((uint)toSquare & 0x3F) << 6) |
                (((uint)movePiece & 0xF) << 12) |
                (((uint)capturedPiece & 0xF) << 16) |
                (((uint)promoPiece & 0xF) << 20) |
                ((epFlag ? 1u : 0u) << 24) |
                ((castle ? 1u : 0u) << 25) |
                ((dblPawn ? 1u : 0u) << 26) |
                (((uint)extra & 0x1F) << 27);
        }

        public int fromSquare => (int)(bitMove & 0x3F);
        public int toSquare => (int)((bitMove >> 6) & 0x3F);
        public int movePiece => (int)((bitMove >> 12) & 0xF);
        public int capturedPiece => (int)((bitMove >> 16) & 0xF);
        public int promoPiece => (int)((bitMove >> 20) & 0xF);
        public bool epFlag => ((bitMove >> 24) & 1) != 0;
        public bool castle => ((bitMove >> 25) & 1) != 0;
        public bool dblPawn => ((bitMove >> 26) & 1) != 0;
        public int extra => (int)((bitMove >> 27) & 0x1F);
    }

}
