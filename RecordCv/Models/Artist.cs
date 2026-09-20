using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecordCv.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Name { get; set; }

        public string? Biography { get; set; }

    }
}
