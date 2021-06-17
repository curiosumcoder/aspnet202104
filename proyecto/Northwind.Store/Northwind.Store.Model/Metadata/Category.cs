using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category : ModelBase
    {
        /// <summary>
        /// Fotografía en formato Base64. Para utilizar en presentación.
        /// </summary>
        [NotMapped]
        public string PictureBase64 { get; set; }


        public partial class CategoryMetadata
        {
            [Display(Name = "Nombre de categoría")]
            public string CategoryName { get; set; }

            [Display(Name = "Descripción")]
            [Required(ErrorMessage = "La {0} es requerida.")]
            [StringLength(32, MinimumLength = 3, ErrorMessage = "La {0} requiere de {2} a {1} caracteres.")]
            public string Description { get; set; }
        }
    }
}
