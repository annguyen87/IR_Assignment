using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Socument
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using System.Windows.Forms;

namespace EduSearch_Information_System
{
    class LuceneApplication
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        public static Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";
       // public Form1 form1;


        public  LuceneApplication()
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
            var form1 = new Form1();
            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                // retrieve the document from the 'ScoreDoc' object
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                ListViewItem item = new ListViewItem(rank.ToString());
                item.SubItems.Add(myFieldValue);
                form1.listView.Items.Add(item);
                
            }
           
        }

    }



}
