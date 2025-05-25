using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Article
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public string AuthorId { get; set; }

        public Doctor Author { get; set; }
    }
}
