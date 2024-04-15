using DomainObjects.Exceptions;
using rm.Trie;
using System.Collections.Concurrent;
using System.Text;

namespace DomainObjects;

public class WordFinder
{
    protected readonly string[] _matrix;
    
    private readonly ITrie _trie;
    private ConcurrentBag<string> _result;
    private int _minWordStream = 0;

    public WordFinder(IEnumerable<string>? matrix)
    {
        ValidateMatrixIsNullOrEmpty(matrix);
        ValidateMatrixSize(matrix!);

        _matrix = ToUpperCaseMatrix(matrix!);
        _trie = new Trie();
        _result = [];
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        AddWordsToTrie(wordstream);

        if (_trie.Count() == 0)
        {
            return _result;
        }
        
        _minWordStream = _trie.GetShortestWords().First().Length;
        
        FindWords();

        return GetSortedResults();
    }

    private void AddWordsToTrie(IEnumerable<string> wordstream)
    {
        var maxLengthAllowed = GetMaxWordLengthForMatrix();

        _trie.Clear();
        foreach (var word in wordstream)
        {
            // Disregard words that exceeds matrix size
            if (word.Length > maxLengthAllowed)
            {
                continue;
            }

            _trie.AddWord(word.ToUpper());
        }
    }

    private void FindWords()
    {
        _result = [];
        
        var tasks = new List<Action>();
        
        for (var i = 0; i < _matrix.Length; i++)
        {
            for (var j = 0; j < _matrix[i].Length; j++)
            {
                // Set local variables to avoid issues with parallel tasks
                var x = i;
                var y = j;
                tasks.Add(() => FindHorizontalWord(x, y));
                tasks.Add(() => FindVerticalWord(x, y));
            }
        }

        Parallel.Invoke(tasks.ToArray());
    }

    private void FindHorizontalWord(int x, int y)
    {
        var charsLeft = _matrix[x].Length - y;

        // Short circuit by the min word to find
        if (charsLeft < _minWordStream) return;

        var node = _trie.GetRootTrieNode();
        var prefix = new StringBuilder();
        
        while (node != null && y < _matrix[x].Length)
        {
            node = node.GetChild(_matrix[x][y]);
            
            if (node == null)
            {
                break;
            }

            prefix.Append(_matrix[x][y]);
            if (node.IsWord)
            {
                _result.Add(prefix.ToString());
            }

            y++;
        }
    }

    private void FindVerticalWord(int x, int y)
    {
        var charsLeft = _matrix.Length - x;

        // Short circuit by the min word to find
        if (charsLeft < _minWordStream) return;

        var node = _trie.GetRootTrieNode();
        var prefix = new StringBuilder();

        while (node != null && x < _matrix.Length)
        {
            node = node.GetChild(_matrix[x][y]);

            if (node == null)
            {
                break;
            }

            prefix.Append(_matrix[x][y]);
            if (node.IsWord)
            {
                _result.Add(prefix.ToString());
            }

            x++;
        }
    }

    private int GetMaxWordLengthForMatrix()
    {
        if (_matrix.Length > _matrix[0].Length)
        {
            return _matrix.Length;
        }

        return _matrix[0].Length;
    }

    private IEnumerable<string> GetSortedResults()
    {
        // Review first if there are results to sort
        if (_result.Count == 0)
        {
            return Array.Empty<string>();
        }

        return _result
            // Group results to count repetead words
            .GroupBy(str => str)
            .Select(g => new
            {
                Word = g.Key,
                Count = g.Count()
            })
            // Order by words counted descendently
            .OrderByDescending(g => g.Count)
            .Select(g => g.Word)
            // Take the top repeated words
            .Take(10)
            // Finally sort alphabetically
            .OrderBy(w => w);
    }

    private static string[] ToUpperCaseMatrix(IEnumerable<string> matrix)
    {
        return matrix
            .Select(str => str.ToUpper())
            .ToArray();
    }

    private static void ValidateMatrixIsNullOrEmpty(IEnumerable<string>? matrix)
    {
        if (matrix == null ||
            matrix.Count() < 1 ||
            matrix.First().Length == 0)
        {
            throw new NullOrEmptyMatrixException();
        }
    }

    private static void ValidateMatrixSize(IEnumerable<string> matrix)
    {
        var width = matrix.First().Length;
        var sameLength = matrix.All(str => str.Length == width);

        if (!sameLength)
        {
            throw new NotSameLengthRowException();
        }

        var height = matrix.Count();
        if (height > 64 || width > 64)
        {
            throw new MaxSizeExceededException();
        }
    }
}