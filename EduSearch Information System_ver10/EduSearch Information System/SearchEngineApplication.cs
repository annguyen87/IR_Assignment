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
using System.IO;

namespace EduSearch_Information_System
{
    public partial class SearchEngineApplication : Form
    {      
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        Similarity newSimilarity;

        public string[] searchResultList;
        public static Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string BIBLIOGRAPHIC_FN = "Bibliographic";
        const string ABSTRACT_FN = "Abstract";
            

        public SearchEngineApplication()
        {
            InitializeComponent();
            searchResultList = new string[1];
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            //analyzer = new Lucene.Net.Analysis.Snowball.SnowballAnalyzer(VERSION);
            newSimilarity = new NewSimilarity();
        }
        /// <summary>
        /// Loads window form
        /// </summary>
        public void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        public void CreateIndex(string indexPath)
        {
            luceneIndexDirectory = FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
            writer.SetSimilarity(newSimilarity);
        }

        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        public void IndexText(string title, string author, string bibliographic, string documentabstract)
        {
 
            Field titleField = new Field(TITLE_FN, title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Field authorField = new Field(AUTHOR_FN, author, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Field bibliographicField = new Field(BIBLIOGRAPHIC_FN, bibliographic, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Field fullabstractField = new Field(ABSTRACT_FN, documentabstract, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Document doc = new Document();
           
            doc.Add(titleField);
            doc.Add(authorField);
            doc.Add(bibliographicField);
            doc.Add(fullabstractField);

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
            searcher.Similarity = newSimilarity;
        }

        /// <summary>
        /// Initialises the parser object
        /// </summary>
        public void CreateParser()
        {
            string[] fields = { TITLE_FN, ABSTRACT_FN };
            var boosts = new Dictionary<string, float>();
            //var boosts = new Dictionary<string, float> { {TITLE_FN, 10 }, { ABSTRACT_FN, 5} };
            parser = new MultiFieldQueryParser(VERSION, fields, analyzer, boosts);
            boosts.Add(TITLE_FN, 5000);
            boosts.Add(ABSTRACT_FN, 10000);
            //parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TITLE_FN, analyzer);
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
            querytext = querytext.ToLower();
         //  var multiFieldQuery = Parse
            Query query = parser.Parse(querytext);
            TopDocs results = searcher.Search(query, 500);
            totalresultLabel.Text = "Total number of result is: " + (results.TotalHits).ToString();
            int i = 0;
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Array.Resize<string>(ref searchResultList, i+1);
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                //string myFieldValue = doc.Get(TEXT_FN).ToString();

                string titleValue = doc.Get(TITLE_FN).ToString();             
                string abstractValue = doc.Get(ABSTRACT_FN).ToString();

                searchResultList[i] = "Q0 " + (scoreDoc.Doc+1) +" "+ rank + " " + scoreDoc.Score + " ";
                i++;              
            }
            
            return results;
        }

        /// <summary>
        /// Pre processing information need
        /// </summary>
        //PorterStemmerAlgorithm.PorterStemmer myStemmer;
        //System.Collections.Generic.Dictionary<string, int> tokenCount;
        public string[] stopWords = { "a", "about", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although",
                                      "always", "am", "among", "amongst", "amount", "an", "and", "another", "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are",
                                      "around", "as", "at","back","be", "been", "before", "beforehand", "behind", "being", "below", "beside", "between", "beyond", "both", "bottom", "but", "by",
                                      "can", "could", "each", "else", "empty", "enough", "etc", "even", "ever", "every", "few", "for", "from", "had", "has", "have", "he", "hence", "her", "here",
                                      "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "him", "himself", "his", "how", "however", "if", "in", "into", "is", "it", "may", "me",
                                      "meanwhile", "might", "mine", "much", "must", "my", "myself", "neither", "never", "nevertheless", "next", "no", "nobody", "none", "nor", "not", "nothing", "now", "nowhere",
                                      "of", "on", "or", "other", "our", "over", "out", "per", "please", "put", "rather", "same", "should", "so", "still", "such", "than", "that", "the", "their", "then", "there",
                                      "these", "they", "this", "those", "through", "thus","to", "un", "up", "very", "via", "was", "what", "when", "where", "which", "while", "who", "whom", "whose", "why", "will",
                                      "with", "would", "yet", "you", "your"}; //list of stopwords

        //public void TextAnalyser()
        //{
        //    myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
        //    tokenCount = new Dictionary<string, int>();
        //}

        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation.
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            char[] splitters = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };

            if (text.Contains("\"") && text.IndexOf('\"') != text.LastIndexOf('\"'))
            {
                string[] stringArray = text.Split('"');
                int i = 0; int j = 0;
                string[] phrasesArray = new string[1];
                string newText = "";
                foreach (var vari in stringArray)
                {
                    if (i % 2 == 0)
                        newText += vari;
                    else
                    {
                        Array.Resize(ref phrasesArray, j + 1);
                        phrasesArray[j++] = "\""+vari+"\"";
                    }

                    i++;
                }
               
                string[] returnArray = newText.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

                int arraylength = returnArray.Length;
                int phrasesLength = phrasesArray.Length;

                Array.Resize(ref returnArray, arraylength + phrasesLength);

                for (i = 0; i < phrasesLength; i++)
                    returnArray[arraylength + i] = phrasesArray[i];

                return returnArray;//text.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            }
            else
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
        //public string[] StemTokens(string[] tokens)
        //{
        //    int numTokens = tokens.Count();
        //    string[] stems = new string[numTokens];
        //    for (int i = 0; i < numTokens; i++)
        //    {
        //        stems[i] = myStemmer.stemTerm(tokens[i]);
        //    }
        //    return stems;
        //}

        /// <summary>
        /// Prints out tokens for a given text string
        /// </summary>
        /// <param name="str">a string of text</param>
        public string OutputTokens(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            string[] tokensnostop = this.StopWordFilter(tokens);

            Console.WriteLine("Tokens: ");
            string outputoken="";
            foreach (string t in tokensnostop)
            {
                outputoken = outputoken + t + " ";
                System.Console.WriteLine(t);
            }
            Console.WriteLine(outputoken);
            return outputoken;
        }

        /// <summary>
        /// Query Expansion
        /// </summary>
        /// <param name="thesaurus"></param>
        /// <param name="queryTerm"></param>
        /// <returns></returns>
        public string GetExpandedQuery(Dictionary<string, string[]> thesaurus, string queryTerm)
        {
            string expandedQuery = "";
            if (thesaurus.ContainsKey(queryTerm))
            {
                string[] array = thesaurus[queryTerm];
                foreach (string a in array)
                {
                    expandedQuery += " " + a;
                }
            }
            return expandedQuery;
        }

        /// <summary>
        /// Update paging footer
        /// </summary>
        public void updatePagingFooter(int numOfPage)
        {
            for (int i = 1; i <= numOfPage; i++)
            {
                cbPagingIndex.Items.Add(i);
            }
        }

        /// <summary>
        /// Outputs results to the screen
        /// </summary>
        /// <param name="results">Search results</param>
        public void DisplayResults(TopDocs results, int pagingIndex, int maxDisplay)
        {
            listView.Items.Clear();
            int offset = pagingIndex * maxDisplay;
            int realDisplay = Math.Min(maxDisplay, results.ScoreDocs.Length - offset);

            string[] delimiter1 = new string[] { ".I", ".T", ".A", ".B", ".W", " ." };
            int rank = offset;
            string[] delimiter2 = new string[] { " ." };
            //for (ScoreDoc scoreDoc in results.ScoreDocs)
            for (int j = offset; j < offset + realDisplay; j++)
            {
                ScoreDoc scoreDoc = results.ScoreDocs[j];
                rank++;
                // retrieve the document from the 'ScoreDoc' object
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string titleValue = doc.Get(TITLE_FN).ToString();
                string authorValue = doc.Get(AUTHOR_FN).ToString();
                string biblioValue = doc.Get(BIBLIOGRAPHIC_FN).ToString();
                string abstractValue = doc.Get(ABSTRACT_FN).ToString();
                string[] array = abstractValue.Split(delimiter2, StringSplitOptions.None);
                ListViewItem item = new ListViewItem(rank.ToString());

                item.SubItems.Add(titleValue);
                item.SubItems.Add(authorValue);
                item.SubItems.Add(biblioValue);
                item.SubItems.Add(array[0]);
                item.SubItems.Add(abstractValue);

                listView.Items.Add(item);
            }
        }



/******THE BELOW PART IS FOR EXECUTING TASKS*****/

        /// <summary>
        /// Browse documents directory and index path
        /// </summary>
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

        /// <summary>
        /// Build index
        /// </summary>
        private void CreateIndexButton_Click(object sender, EventArgs e)
        {
            // Calculate time excution
            var watch = System.Diagnostics.Stopwatch.StartNew();

            CreateIndex(IndexPathLabel.Text);

            List<string> l = new List<string>();
            for (int i = 1; i <= 1400; i++)
            {
                string text = System.IO.File.ReadAllText(SourcePathLabel.Text + @"\" + i + ".txt");
                l.Add(@text);
            }

            string[] delimiter1 = new string[] { ".I", ".T", ".A", ".B", ".W" };
            string[] delimiter2 = new string[] { " ." };
            foreach (string s in l)
            {
                string[] array = s.Split(delimiter1, StringSplitOptions.None);
               // string documentabstract = string.Join(" ", array.Skip(7));
                string[] documentabstract = array[5].Split(delimiter2, StringSplitOptions.None);
                string abs = string.Join(" .", documentabstract.Skip(1));
                IndexText(array[2], array[3], array[4], abs);//documentabstract);
                 //IndexText(array[1], array[3], array[4], array[5]);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            TimeExcutionLabel.Text = "Index takes " + elapsedMs.ToString() + " milisecond";

            CleanUpIndexer();
        }

        /// <summary>
        /// Process user information need
        /// </summary>
        private void InformationNeedButton_Click(object sender, EventArgs e)
        {
            OutputTokens(SearchBox.Text);
        }

        /// <summary>
        /// Initiate search process
        /// </summary>
        private void SearchButton_Click(object sender, EventArgs e)
        {          
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string finalQuery = OutputTokens(SearchBox.Text);
            CreateSearcher();
            CreateParser();
            txtBox_FinalQuery.Text = finalQuery;
            results = SearchIndex(finalQuery);

            //calculating paging index
            int numOfPage = (int)Math.Ceiling((double)results.ScoreDocs.Length / (double)maxDisplay);

            DisplayResults(results, 0, maxDisplay);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SearchTimeExcutionLabel.Text = "Search takes " + elapsedMs.ToString() + " milisecond";

            //update paging footer
            cbPagingIndex.Items.Clear();
            try
            {
                cbPagingIndex.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

            }
            for (int i = 0; i < numOfPage; i++)
            {
                cbPagingIndex.Items.Add("Page " + (i + 1));
            }
        }

        /// <summary>
        /// Store the result of the most recent Search Button click
        /// </summary>
        TopDocs results;
        int maxDisplay = 10;
       
        /// <summary>
        /// Browsing the search result
        /// </summary>
        private void cbPagingIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayResults(results, cbPagingIndex.SelectedIndex, maxDisplay);
        }
        
        /// <summary>
        /// Display full document information
        /// </summary>
        private void listView_ItemActivate(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {              
                ListViewItem item = listView.SelectedItems[0];
                MessageBox.Show("Title: " + item.SubItems[1].Text + "\nAuthor: " + item.SubItems[2].Text  + "\nBibliography: " + item.SubItems[3].Text + "\n\nAbstract:\n" + item.SubItems[5].Text);
                //txtTitle.Text = item.SubItems[1].Text;//Title
                //txtAuthor.Text = item.SubItems[2].Text;//Author
                //txtBib.Text = item.SubItems[3].Text; // Bib
                //txtAbs.Text = item.SubItems[5].Text; //Abstract on index 5
            }
        }

        /// <summary>
        /// Save search result to a file for evaluation
        /// </summary>
        private void btn_SaveFile_Click(object sender, EventArgs e)
        {
            string fileName = txtBox_FileName.Text+".txt";
            string ID_SavedFile = txtBox_Identifier.Text;

            using (StreamWriter textWriter = new StreamWriter(fileName, true))
            {
                foreach (string var in searchResultList)
                {
                    textWriter.Write(ID_SavedFile + " ");
                    textWriter.Write(var);
                    textWriter.WriteLine("n9535250_TeamIR");
                }
            }
            MessageBox.Show("Results Saved!!");
        }

        /// <summary>
        /// Clear query for new search
        /// </summary>
        private void btn_ClearQuery_Click(object sender, EventArgs e)
        {
            SearchBox.Text = "";
            txtBox_FinalQuery.Text = "";
            listView.Items.Clear();
            this.SearchTimeExcutionLabel.Text = "";
            this.totalresultLabel.Text = "";
        }
    }
}
