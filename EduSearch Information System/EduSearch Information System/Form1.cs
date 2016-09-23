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
using Lucene.Net.Documents; // for Socument
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser



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


        // MARK - Index and Search term


        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        public static Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";
        // public Form1 form1;


        public void LuceneApplication()
        {
            luceneIndexDirectory = null; // Is set in Create Index
            analyzer = null;  // Is set in CreateAnalyser
            writer = null; // Is set in CreateWriter
        }
        public void CreateIndex(string indexPath)
        {
            // TODO: Enter code to create the Lucene Index 
            luceneIndexDirectory = FSDirectory.Open(indexPath);
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            IndexDeletionPolicy p;

            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
        }
        public void CreateAnalyser()
        {
            // TODO: Enter code to create the Lucene Analyser 
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();

        }


        /// <summary>
        /// Add the text to the index
        /// </summary>
        /// <param name="text">The text tio index</param>
        public void IndexText(string text)
        {

            // TODO: Enter code to index text
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
            TopDocs results = searcher.Search(query, 100);
            System.Console.WriteLine("Number of results is " + results.TotalHits);
            return results;

        }


        // Activity 7
        /// <summary>
        /// Outputs results to the screen
        /// </summary>
        /// <param name="results">Search results</param>
        public void DisplayResults(TopDocs results)
        {
            
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                // retrieve the document from the 'ScoreDoc' object
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                ListViewItem item = new ListViewItem(rank.ToString());
                item.SubItems.Add(myFieldValue);
                listView.Items.Add(item);

            }

        }



        //MARK - Processing text

        PorterStemmerAlgorithm.PorterStemmer myStemmer; // for Activity 4,5
        System.Collections.Generic.Dictionary<string, int> tokenCount; // for Activity 5
        public string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with" }; // for challange activity

        public void TextAnalyser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            tokenCount = new Dictionary<string, int>();
        }

        //Activity 3
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

        //Activity 4
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
        /// Outputs stems to the sceen
        /// </summary>
        /// <param name="str">A string of text to be stemmed</param>
        public void OutputStems(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            string[] stems = StemTokens(tokens);
            Console.WriteLine("Stems: ");
            foreach (string s in stems)
            {
                System.Console.WriteLine(s);
            }
        }

        // Activity 5
        /// <summary>
        /// Counts the occurrences of a given set of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        public void CountOccurrences(string[] tokens)
        {
            foreach (string s in tokens)
            {
                int count = 0;
                tokenCount.TryGetValue(s, out count);
                count = count + 1;
                tokenCount[s] = count;
            }
        }

        /// <summary>
        /// Displays the count of tokens to the screen
        /// </summary>
        public void DisplayCount()
        {
            foreach (var value in tokenCount)
            {
                string token = value.Key;
                int occurrences = value.Value;

                System.Console.WriteLine("Token is " + token + " number of occurrences are " + occurrences);
            }
        }

        /// <summary>
        /// Outputs occurrence count for a given string
        /// </summary>
        /// <param name="str">A string of text</param>
        public void OutputTokenCount(string str)
        {
            ResetCount();
            string[] tokens = TokeniseString(str);
            string[] stems = StemTokens(tokens);
            Console.Out.WriteLine("Orginal: " + str);
            CountOccurrences(stems);
            DisplayCount();
        }



        /// <summary>
        /// Resets the tokencount to 0
        /// </summary>
        public void ResetCount()
        {
            tokenCount.Clear();
        }



        // Activity 6
        /// <summary>
        /// Saves the token count to a file
        /// </summary>
        /// <param name="filename">The filename to save the token count to</param>
        public void SaveCountToFile(string filename)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filename);
            foreach (var value in tokenCount)
            {
                string token = value.Key;
                int occurrence = value.Value;
                writer.WriteLine("Token is " + token + " number of occurrences are " + occurrence);
            }
            writer.Flush();
            writer.Close();
        }


        /// <summary>
        /// Counts the occurrence of stems in a given text file and prints out the results to the screen
        /// </summary>
        /// <param name="infilename">The filename to be read in</param>
        /// <param name="outfilename">The filename to save the statistics</param>
        public void ProcessText(string infilename, string outfilename)
        {
            ResetCount();
            System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                //string text = linel
                string[] tokens = TokeniseString(line);
                string[] stems = StemTokens(tokens);
                CountOccurrences(stems);
            }
            reader.Close();
            DisplayCount();
            SaveCountToFile(outfilename);

        }


        // Challenge Activity
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
        /// Outputs the token count without any stopwords
        /// </summary>
        /// <param name="strs"></param>
        public void OutputTokenCountWithoutStopwords(string str)
        {
            ResetCount();

            string[] tokens = TokeniseString(str);
            string[] tokensNoStop = StopWordFilter(tokens);
            string[] stems = StemTokens(tokensNoStop);
            CountOccurrences(stems);

            DisplayCount();
        }


        /// <summary>
        /// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
        /// </summary>
        /// <param name="infilename">The filename to be read in</param>
        /// <param name="outfilename">The filename to save the statistics</param>
        public void ProcessTextNoStopWords(string infilename, string outfilename)
        {
            ResetCount();
            System.IO.StreamReader reader = new System.IO.StreamReader(infilename);
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                //string text = linel
                string[] tokens = TokeniseString(line);
                string[] tokensnostop = this.StopWordFilter(tokens);
                string[] stems = StemTokens(tokensnostop);
                CountOccurrences(stems);
            }
            reader.Close();
            DisplayCount();
            SaveCountToFile(outfilename);

        }










        // MARK - Main 

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
            SearchIndex(SearchBox.Text);
            DisplayResults(SearchIndex(SearchBox.Text));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SearchTimeExcutionLabel.Text = "Search takes " + elapsedMs.ToString() + " milisecond";
        }

        private void InformationNeedButton_Click(object sender, EventArgs e)
        {
            OutputTokens(SearchBox.Text);
           
           // ProcessTextNoStopWords()
        }
    }
}