using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiyLayer.Entities
{
    public class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
