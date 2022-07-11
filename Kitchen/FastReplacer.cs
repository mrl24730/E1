﻿using System;
using System.Collections.Generic;
using System.Text;


//COPY from http://www.codeproject.com/Articles/298519/Fast-Token-Replacement-in-Csharp


namespace Kitchen
{
    /// <summary>
    /// FastReplacer is a utility class similar to StringBuilder, with fast Replace function.
    /// FastReplacer is limited to replacing only properly formatted tokens.
    /// Use ToString() function to get the final text.
    /// </summary>
    public class FastReplacer
    {
        public readonly string TokenOpen;
        public readonly string TokenClose;

        /// <summary>
        /// All tokens that will be replaced must have same opening and closing delimiters, such as "{" and "}".
        /// </summary>
        /// <param name="tokenOpen">Opening delimiter for tokens.</param>
        /// <param name="tokenClose">Closing delimiter for tokens.</param>
        /// <param name="caseSensitive">Set caseSensitive to false to use case-insensitive search when replacing tokens.</param>
        public FastReplacer(string tokenOpen, string tokenClose, bool caseSensitive = true)
        {
            if (string.IsNullOrEmpty(tokenOpen) || string.IsNullOrEmpty(tokenClose))
                throw new ArgumentException("Token must have opening and closing delimiters, such as \"{\" and \"}\".");

            TokenOpen = tokenOpen;
            TokenClose = tokenClose;

            var stringComparer = caseSensitive ? StringComparer.Ordinal : StringComparer.InvariantCultureIgnoreCase;
            OccurrencesOfToken = new Dictionary<string, List<TokenOccurrence>>(stringComparer);
        }

        private readonly FastReplacerSnippet RootSnippet = new FastReplacerSnippet("");

        private class TokenOccurrence
        {
            public FastReplacerSnippet Snippet;
            public int Start; // Position of a token in the snippet.
            public int End; // Position of a token in the snippet.
        }

        private readonly Dictionary<string, List<TokenOccurrence>> OccurrencesOfToken;

        public void Append(string text)
        {
            FastReplacerSnippet s = new FastReplacerSnippet(text);
            RootSnippet.Append(s);
            ExtractTokens(s);
        }

        /// <returns>Returns true if the token was found, false if nothing was replaced.</returns>
        public bool Replace(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences))
            {
                OccurrencesOfToken.Remove(token);
                FastReplacerSnippet s = new FastReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.Replace(occurrence.Start, occurrence.End, s);
                ExtractTokens(s);
                return occurrences.Count > 0;
            }
            return false;
        }

        /// <returns>Returns true if the token was found, false if nothing was replaced.</returns>
        public bool InsertBefore(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences))
            {
                FastReplacerSnippet s = new FastReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.InsertBefore(occurrence.Start, s);
                ExtractTokens(s);
                return occurrences.Count > 0;
            }
            return false;
        }

        /// <returns>Returns true if the token was found, false if nothing was replaced.</returns>
        public bool InsertAfter(string token, string text)
        {
            ValidateToken(token, text, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences))
            {
                FastReplacerSnippet s = new FastReplacerSnippet(text);
                foreach (var occurrence in occurrences)
                    occurrence.Snippet.InsertAfter(occurrence.End, s);
                ExtractTokens(s);
                return occurrences.Count > 0;
            }
            return false;
        }

        public bool Contains(string token)
        {
            ValidateToken(token, token, false);
            List<TokenOccurrence> occurrences;
            if (OccurrencesOfToken.TryGetValue(token, out occurrences))
                return occurrences.Count > 0;
            return false;
        }

        private void ExtractTokens(FastReplacerSnippet snippet)
        {
            int last = 0;
            while (last < snippet.Text.Length)
            {
                int start = snippet.Text.IndexOf(TokenOpen, last);
                if (start == -1)
                    return;
                int end = snippet.Text.IndexOf(TokenClose, start + TokenOpen.Length);
                if (end == -1)
                    throw new ArgumentException(string.Format("Token is opened but not closed in text \"{0}\".", snippet.Text));
                end += TokenClose.Length;

                string token = snippet.Text.Substring(start, end - start);
                string context = snippet.Text;
                ValidateToken(token, context, true);


                TokenOccurrence tokenOccurrence = new TokenOccurrence { Snippet = snippet, Start = start, End = end };
                List<TokenOccurrence> occurrences;
                if (OccurrencesOfToken.TryGetValue(token, out occurrences))
                    occurrences.Add(tokenOccurrence);
                else
                    OccurrencesOfToken.Add(token, new List<TokenOccurrence> { tokenOccurrence });

                last = end;
            }
        }

        private void ValidateToken(string token, string context, bool alreadyValidatedStartAndEnd)
        {
            if (!alreadyValidatedStartAndEnd)
            {
                if (!token.StartsWith(TokenOpen))
                    throw new ArgumentException(string.Format("Token \"{0}\" shoud start with \"{1}\". Used with text \"{2}\".", token, TokenOpen, context));
                int closePosition = token.IndexOf(TokenClose);
                if (closePosition == -1)
                    throw new ArgumentException(string.Format("Token \"{0}\" should end with \"{1}\". Used with text \"{2}\".", token, TokenClose, context));
                if (closePosition !=  token.Length - TokenClose.Length)
                    throw new ArgumentException(string.Format("Token \"{0}\" is closed before the end of the token. Used with text \"{1}\".", token, context));
            }

            if (token.Length == TokenOpen.Length + TokenClose.Length)
                throw new ArgumentException(string.Format("Token has no body. Used with text \"{0}\".", context));
            if (token.Contains("\n"))
                throw new ArgumentException(string.Format("Unexpected end-of-line within a token. Used with text \"{0}\".", context));
            if (token.IndexOf(TokenOpen, TokenOpen.Length) != -1)
                throw new ArgumentException(string.Format("Next token is opened before a previous token was closed in token \"{0}\". Used with text \"{1}\".", token, context));
        }

        public override string ToString()
        {
            int totalTextLength = RootSnippet.GetLength();
            StringBuilder sb = new StringBuilder(totalTextLength);
            RootSnippet.ToString(sb);
            if (sb.Length != totalTextLength)
                throw new InvalidOperationException(string.Format(
                    "Internal error: Calculated total text length ({0}) is different from actual ({1}).",
                    totalTextLength, sb.Length));
            return sb.ToString();
        }
    }
}
