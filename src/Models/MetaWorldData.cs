using System;

namespace MetaWorldWeb.Models
{
    /// <summary>
    /// The base schema representing Meta World data retrieved from a web page
    /// </summary>
    public class MetaWorldData
    {
        // Page Basics

        /// <summary>
        /// Title of world retrieved. This is the title of the web page accessed unless overridden by meta data.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Uri of the world retrieved.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// The Data and Time in UTC that the world was retrieved.
        /// </summary>
        public DateTime Retrieved { get; set; }

        /// <summary>
        /// A raw binary SHA256 hash of the web page.
        /// </summary>
        public byte[] PageSHA256Hash { get; set; }

        // Page Data

        /// <summary>
        /// The world hashes retrieved from the web page. The number is equal to the number requested, and remains constant
        /// while the respective sub section of the web page remians the same.
        /// </summary>
        public ulong[] Hashes { get; set; }

        /// <summary>
        /// An array of all the words found within the page content (or provided by meta tags). Headers and scripts are ignored.
        /// This can be used for training procedural word generators such as Markov Generators.
        /// </summary>
        public string[] WordList { get; set; }

        /// <summary>
        /// An array of the Links on the web page.
        /// </summary>
        public LinkData[] Links { get; set; }

        /// <summary>
        /// An array of Forms and their data on the web page.
        /// </summary>
        public FormData[] Forms { get; set; }

        /// <summary>
        /// An array of images on the web page.
        /// </summary>
        public ImageData[] Images { get; set; }

    }
}
