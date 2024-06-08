namespace TrieDictionaryTest;

[TestClass]
public class TrieTest
{
    // Test that a word can be inserted into the trie
    [TestMethod]
    public void TestInsertWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";

        // Act
        trie.Insert(word);

        // Assert
        Assert.IsTrue(trie.Search(word));
    }

    // Test that a word can be deleted from the trie
    [TestMethod]
    public void TestDeleteWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Act
        trie.Delete(word);

        // Assert
        Assert.IsFalse(trie.Search(word));
    }

    // Test that a word is not inserted into the trie if it already exists
    [TestMethod]
    public void TestInsertExistingWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Assert
        Assert.IsFalse(trie.Insert(word));
    }

    // Test that a word is not deleted from the trie if it does not exist
    [TestMethod]
    public void TestDeleteNonExistentWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";

        // Assert
        Assert.IsFalse(trie.Delete(word));
    }

    // Test that a word is not found in the trie if it does not exist
    [TestMethod]
    public void TestSearchNonExistentWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";

        // Assert
        Assert.IsFalse(trie.Search(word));
    }

    // Test that a word is found in the trie if it exists
    [TestMethod]
    public void TestSearchWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Assert
        Assert.IsTrue(trie.Search(word));
    }

    // Test that a word is not found in the trie if it is a prefix of another word
    [TestMethod]
    public void TestSearchPrefix()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Assert
        Assert.IsFalse(trie.Search("hell"));
    }

    // Test that a word is not found in the trie if it is a suffix of another word
    [TestMethod]
    public void TestSearchSuffix()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Assert
        Assert.IsFalse(trie.Search("lo"));
    }

    // Test that a word is deleted from the trie if it is a prefix of another word
    [TestMethod]
    public void TestDeletePrefix()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Act
        string prefix = "hell";
        trie.Insert(prefix);
        trie.Delete(prefix);

        // Assert
        Assert.IsFalse(trie.Search(prefix));
        Assert.IsTrue(trie.Search(word));
    }

    // Test AutoSuggest method for the prefix "cat" not present in the
    // trie containing the words "catastrophe", "catatonic", and "caterpillar"
    [TestMethod]
    public void TestAutoSuggest()
    {
        // Arrange
        Trie trie = new Trie();
        string[] words = { "catastrophe", "catatonic", "caterpillar" };
        foreach (string word in words)
        {
            trie.Insert(word);
        }

        // Act
        List<string> suggestions = trie.AutoSuggest("cat");

        // Assert
        Assert.AreEqual(3, suggestions.Count);
        // Assert suggestions are sorted alphabetically
        Assert.AreEqual("catastrophe", suggestions[0]);
        Assert.AreEqual("catatonic", suggestions[1]);
        Assert.AreEqual("caterpillar", suggestions[2]);
    }

    // Test GetSpellingSuggestions method for a word not present in the trie
    [TestMethod]
    public void TestGetSpellingSuggestions()
    {
        // Arrange
        Trie trie = new Trie();
        string[] words = { "catastrophe", "catatonic", "caterpillar" };
        foreach (string word in words)
        {
            trie.Insert(word);
        }

        // Act
        List<string> suggestions = trie.GetSpellingSuggestions("catatnoic");

        // Assert
        Assert.AreEqual(1, suggestions.Count);
        Assert.AreEqual("catatonic", suggestions[0]);
    }
}