using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public enum Categorie { Document, Image }
    public class DocumentViewModel
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string Path { get; set; }
        public Categorie categorie { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime DateCreation { get; set; }
        public Project Project { get; set; }
        public int ProjectFK { get; set; }
        public int Size { get; set; }
    }
}