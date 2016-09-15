﻿using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(p_ContactMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class p_Contact
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class p_ContactMetadata
    {
     //   [DisplayFormat(ConvertEmptyStringToNull = false)]
       // [Required(ErrorMessage = Messages.U_001)]
       // [StringLength(8, MinimumLength = 30, ErrorMessage = Messages.U_003)]
       // public string id_Contact;


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        public string Prenom;

        [Display(Name = "Contact")]
        public string Nom;
    }

}