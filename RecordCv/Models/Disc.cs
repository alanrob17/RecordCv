using System;
using System.Collections.Generic;
using System.Text;

namespace RecordCv.Models
{
    public class Disc
    {
        public int DiscId { get; set; }
        public int RecordId { get; set; }
        public int DiscNo { get; set; }
        public int? FreeDbDiscId { get; set; }
        public string? FreeDbId { get; set; }
        public int? Length { get; set; }
    }
}
