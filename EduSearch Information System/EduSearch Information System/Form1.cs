/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Socument
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using EduSearch_Information_System;


namespace EduSearch_Information_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public void Form1_Load(object sender, EventArgs e)
        {
            
        }
        LuceneApplication myLuceneapp = new LuceneApplication();
        

        private void BrowseSourceButton_Click(object sender, EventArgs e)
        {
            SourceBrowserDialog.ShowDialog();
            SourcePathLabel.Text = SourceBrowserDialog.SelectedPath;
        }

        private void BrowseIndexButton_Click(object sender, EventArgs e)
        {
            IndexBrowseDialog.ShowDialog();
            IndexPathLabel.Text = IndexBrowseDialog.SelectedPath;
        }

        private void CreateIndexButton_Click(object sender, EventArgs e)
        {
            // Calculate time excution
            var watch = System.Diagnostics.Stopwatch.StartNew();
           
            myLuceneapp.CreateIndex(IndexPathLabel.Text);
            myLuceneapp.CreateAnalyser();
           
            List<string> l = new List<string>();
            for (int i = 1; i <= 1400; i++)
            {
                 string text = System.IO.File.ReadAllText(SourcePathLabel.Text+ @"\"+i+".txt"); 
                 l.Add(@text);
            }

            foreach (string s in l)
            {
                myLuceneapp.IndexText(s);
            }
           // CleanUp();
            

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TimeExcutionLabel.Text = "Index takes " + elapsedMs.ToString() + " milisecond";
            myLuceneapp.CleanUpIndexer();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            myLuceneapp.CreateSearcher();
            myLuceneapp.CreateParser();
            myLuceneapp.SearchIndex(SearchBox.Text);
            myLuceneapp.DisplayResults(myLuceneapp.SearchIndex(SearchBox.Text));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SearchTimeExcutionLabel.Text = "Search takes " + elapsedMs.ToString() + " milisecond";
        }
    }
}
*/

///// <summary>
///// Outputs stems to the sceen
///// </summary>
///// <param name="str">A string of text to be stemmed</param>
//public void OutputStems(string str)
//{
//    System.Console.WriteLine("Orginal: \"" + str + "\"");
//    string[] tokens = TokeniseString(str);
//    string[] stems = StemTokens(tokens);
//    Console.WriteLine("Stems: ");
//    foreach (string s in stems)
//    {
//        System.Console.WriteLine(s);
//    }
//}

//// Activity 5
///// <summary>
///// Counts the occurrences of a given set of tokens
///// </summary>
///// <param name="tokens">An array of tokens</param>
//public void CountOccurrences(string[] tokens)
//{
//    foreach (string s in tokens)
//    {
//        int count = 0;
//        tokenCount.TryGetValue(s, out count);
//        count = count + 1;
//        tokenCount[s] = count;
//    }
//}

///// <summary>
///// Displays the count of tokens to the screen
///// </summary>
//public void DisplayCount()
//{
//    foreach (var value in tokenCount)
//    {
//        string token = value.Key;
//        int occurrences = value.Value;

//        System.Console.WriteLine("Token is " + token + " number of occurrences are " + occurrences);
//    }
//}

///// <summary>
///// Outputs occurrence count for a given string
///// </summary>
///// <param name="str">A string of text</param>
//public void OutputTokenCount(string str)
//{
//    ResetCount();
//    string[] tokens = TokeniseString(str);
//    string[] stems = StemTokens(tokens);
//    Console.Out.WriteLine("Orginal: " + str);
//    CountOccurrences(stems);
//    DisplayCount();
//}



///// <summary>
///// Resets the tokencount to 0
///// </summary>
//public void ResetCount()
//{
//    tokenCount.Clear();
//}



//// Activity 6
///// <summary>
///// Saves the token count to a file
///// </summary>
///// <param name="filename">The filename to save the token count to</param>
//public void SaveCountToFile(string filename)
//{
//    System.IO.StreamWriter writer = new System.IO.StreamWriter(filename);
//    foreach (var value in tokenCount)
//    {
//        string token = value.Key;
//        int occurrence = value.Value;
//        writer.WriteLine("Token is " + token + " number of occurrences are " + occurrence);
//    }
//    writer.Flush();
//    writer.Close();
//}


///// <summary>
///// Counts the occurrence of stems in a given text file and prints out the results to the screen
///// </summary>
///// <param name="infilename">The filename to be read in</param>
///// <param name="outfilename">The filename to save the statistics</param>
//public void ProcessText(string infilename, string outfilename)
//{
//    ResetCount();
//    System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
//    string line = "";
//    while ((line = reader.ReadLine()) != null)
//    {
//        //string text = linel
//        string[] tokens = TokeniseString(line);
//        string[] stems = StemTokens(tokens);
//        CountOccurrences(stems);
//    }
//    reader.Close();
//    DisplayCount();
//    SaveCountToFile(outfilename);

//}


///// <summary>
///// Outputs the token count without any stopwords
///// </summary>
///// <param name="strs"></param>
//public void OutputTokenCountWithoutStopwords(string str)
//{
//    ResetCount();

//    string[] tokens = TokeniseString(str);
//    string[] tokensNoStop = StopWordFilter(tokens);
//    string[] stems = StemTokens(tokensNoStop);
//    CountOccurrences(stems);

//    DisplayCount();
//}


///// <summary>
///// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
///// </summary>
///// <param name="infilename">The filename to be read in</param>
///// <param name="outfilename">The filename to save the statistics</param>
//public void ProcessTextNoStopWords(string infilename, string outfilename)
//{
//    ResetCount();
//    System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
//    string line = "";
//    while ((line = reader.ReadLine()) != null)
//    {
//        //string text = linel
//        string[] tokens = TokeniseString(line);
//        string[] tokensnostop = this.StopWordFilter(tokens);
//        string[] stems = StemTokens(tokensnostop);
//        CountOccurrences(stems);
//    }
//    reader.Close();
//    DisplayCount();
//    SaveCountToFile(outfilename);

//}

//**********************************************************************************//
//MAKE SURE YOUR CODE WORKED BEFORE PASSING THIS LINE
//ANY TUTORIAL CODE MUST BE COMMENTED AND STAY ABOVE THIS LINE FOR FUTURE REFERENCE
//CODE UNDER THIS LINE
//*********************************************************************************//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Documents
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using Lucene.Net.Analysis.Snowball;



namespace EduSearch_Information_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

/// <summary>
/// Create Index, Create queries (terms and phrases), Submit queries, Display results
/// </summary>

        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        public static Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";
        
        public void LuceneApplication()
        {
            luceneIndexDirectory = null; // Is set in Create Index
            analyzer = new Lucene.Net.Analysis.WhitespaceAnalyzer();
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();
            analyzer = new Lucene.Net.Analysis.StopAnalyzer(VERSION);
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            analyzer = new Lucene.Net.Analysis.Snowball.SnowballAnalyzer(VERSION, "English");
            writer = null; // Is set in CreateWriter

        }
        public void CreateIndex(string indexPath)
        {
            luceneIndexDirectory = FSDirectory.Open(indexPath);
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            IndexDeletionPolicy p;

            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
        }
        public void CreateAnalyser()
        {
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();
        }

        /// <summary>
        /// Add the text to the index
        /// </summary>
        /// <param name="text">The text to index</param>
        public void IndexText(string text)
        {
            //System.Console.WriteLine("Indexing " + text);
            Lucene.Net.Documents.Field field = new Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            //  Lucene.Net.Documents.Field field = new Field("Text", text, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(field);
            writer.AddDocument(doc);
        }

        /// <summary>
        /// Flushes buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }
        /// <summary>
        /// Initialises the searcher object
        /// </summary>
        public void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
        }

        /// <summary>
        /// Initialises the parser object
        /// </summary>
        public void CreateParser()
        {
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
        }

        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearch()
        {
            searcher.Dispose();
        }

        public TopDocs SearchIndex(string querytext)
        {

            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);
            TopDocs results = searcher.Search(query, 10);
            totalresultLabel.Text = "Total number of result is" + (results.TotalHits).ToString();
           
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
            }

            return results;
        }

/// <summary>
/// Pre processing information need
/// </summary>
        PorterStemmerAlgorithm.PorterStemmer myStemmer;
        System.Collections.Generic.Dictionary<string, int> tokenCount;
        public string[] stopWords = { "a", "about", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although",
                                      "always", "am", "among", "amongst", "amount", "an", "and", "another", "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are",
                                      "around", "as", "at","back","be", "been", "before", "beforehand", "behind", "being", "below", "beside", "between", "beyond", "both", "bottom", "but", "by",
                                      "can", "could", "each", "else", "empty", "enough", "etc", "even", "ever", "every", "few", "for", "from", "had", "has", "have", "he", "hence", "her", "here",
                                      "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "him", "himself", "his", "how", "however", "if", "in", "into", "is", "it", "may", "me",
                                      "meanwhile", "might", "mine", "much", "must", "my", "myself", "neither", "never", "nevertheless", "next", "no", "nobody", "none", "nor", "not", "nothing", "now", "nowhere",
                                      "of", "on", "or", "other", "our", "over", "out", "per", "please", "put", "rather", "same", "should", "so", "still", "such", "than", "that", "the", "their", "then", "there",
                                      "these", "they", "this", "those", "through", "thus","to", "un", "up", "very", "via", "was", "what", "when", "where", "which", "while", "who", "whom", "whose", "why", "will",
                                      "with", "would", "yet", "you", "your"}; //list of stopwords

        public void TextAnalyser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            tokenCount = new Dictionary<string, int>();
        }

        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            char[] splitters = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };
            return text.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// Removes stopwords from an array of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        /// <returns>The array of tokens without any stopwords</returns>
        public string[] StopWordFilter(string[] tokens)
        {

            int numTokens = tokens.Count();
            List<string> filteredTokens = new List<string>();
            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                if (!stopWords.Contains(token) && (token.Length > 2)) filteredTokens.Add(token);
            }
            return filteredTokens.ToArray<string>();
        }

        /// <summary>
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        public string[] StemTokens(string[] tokens)
        {
            int numTokens = tokens.Count();
            string[] stems = new string[numTokens];
            for (int i = 0; i < numTokens; i++)
            {
                stems[i] = myStemmer.stemTerm(tokens[i]);
            }
            return stems;
        }

        /// <summary>
        /// Prints out tokens for a given text string
        /// </summary>
        /// <param name="str">a string of text</param>
        public void OutputTokens(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            string[] tokensnostop = this.StopWordFilter(tokens);

            Console.WriteLine("Tokens: ");
            foreach (string t in tokensnostop)
            {
                var items = checkedListBox.Items;
                items.Add(t);

                System.Console.WriteLine(t);
            }
        }

        /// <summary>
        /// Outputs results to the screen
        /// </summary>
        /// <param name="results">Search results</param>
        public void DisplayResults(TopDocs results)
        {

            string[] delimiter1 = new string[] { ".I", ".T", ".A", ".B", ".W", " ." };
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                // retrieve the document from the 'ScoreDoc' object
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                string[] array = myFieldValue.Split(delimiter1, StringSplitOptions.None);
                ListViewItem item = new ListViewItem(rank.ToString());
                foreach (string entry in array)
                {
                    string title = array[2];
                    string author = array[4];
                    string bibliographic = array[5];
                    string firstsentence = array[6];
                    string fullabstract = "";
                    for (int i = 6; i < array.Length; i++) { 
                        fullabstract = fullabstract + array[i] +" .";
                     }
                    item.SubItems.Add(title);
                    item.SubItems.Add(author);
                    item.SubItems.Add(bibliographic);
                    item.SubItems.Add(firstsentence);
                    item.SubItems.Add(fullabstract);
                    Console.WriteLine(entry);
                }
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Executing GUI Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseSourceButton_Click(object sender, EventArgs e)
        {
            SourceBrowserDialog.ShowDialog();
            SourcePathLabel.Text = SourceBrowserDialog.SelectedPath;
        }

        private void BrowseIndexButton_Click(object sender, EventArgs e)
        {
            IndexBrowseDialog.ShowDialog();
            IndexPathLabel.Text = IndexBrowseDialog.SelectedPath;
        }

        private void CreateIndexButton_Click(object sender, EventArgs e)
        {
            // Calculate time excution
            var watch = System.Diagnostics.Stopwatch.StartNew();

            CreateIndex(IndexPathLabel.Text);
            CreateAnalyser();

            List<string> l = new List<string>();
            for (int i = 1; i <= 1400; i++)
            {
                string text = System.IO.File.ReadAllText(SourcePathLabel.Text + @"\" + i + ".txt");
                l.Add(@text);
            }

            foreach (string s in l)
            {
                IndexText(s);
            }
            // CleanUp();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TimeExcutionLabel.Text = "Index takes " + elapsedMs.ToString() + " milisecond";
            CleanUpIndexer();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string finalQuery = "";
            foreach (Object item in checkedListBox.CheckedItems)
            {
                finalQuery = finalQuery + item + " ";
            }
            SearchBox.Text = finalQuery;
            CreateSearcher();
            CreateParser();
            DisplayResults(SearchIndex(SearchBox.Text));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SearchTimeExcutionLabel.Text = "Search takes " + elapsedMs.ToString() + " milisecond";
        }

        private void InformationNeedButton_Click(object sender, EventArgs e)
        {
            OutputTokens(SearchBox.Text);
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0) {
                ListViewItem item = listView.SelectedItems[0];
                txtTitle.Text = item.SubItems[1].Text;
                txtAuthor.Text = item.SubItems[2].Text;
                txtBib.Text = item.SubItems[3].Text;
                txtAbs.Text = item.SubItems[5].Text;
                

            }
        }
    }
}