﻿using System;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ExTween.Art;
using Xunit;

namespace TestExTween.Art
{
    [UseReporter(typeof(RiderReporter))]
    [UseApprovalSubdirectory("ApprovalTests")]
    public class SymbolRenderingApprovals
    {
        [Fact]
        public void render_every_letter_minimum_fidelity()
        {
            VerifyEveryChar(char.IsLetter);
        }
        
        [Fact]
        public void render_every_symbol_minimum_fidelity()
        {
            VerifyEveryChar(char.IsSymbol);
        }
        
        [Fact]
        public void render_every_punctuation_minimum_fidelity()
        {
            VerifyEveryChar(char.IsPunctuation);
        }
        
        [Fact]
        public void render_every_digit_minimum_fidelity()
        {
            VerifyEveryChar(char.IsDigit);
        }

        private void VerifyEveryChar(Func<char, bool> condition)
        {
            string allLetters = string.Empty;
            var font = new MonospaceFont(30);

            for (int i = 32; i < 255; i++)
            {
                var c = (char) i;
                if (condition(c))
                {
                    allLetters += $"-- {c} --\n";
                    var glyph = font.GetTweenGlyphForLetter(c);
                    glyph.RenderOffset = glyph.Size / 2;
                    glyph.NumberOfSegments = 0;

                    var painter = new AsciiPainter(glyph.Size.ToIntXy() + new IntXyPair(1, 0));
                    glyph.Draw(painter);
                    allLetters += painter.RenderString() + "\n\n";
                }
            }
            
            Approvals.Verify(allLetters);
        }
    }
}
