﻿using System;
using System.Diagnostics;

namespace sachem.Models
{
    public class Messages
    {
        #region messagesInformatifs

        /// <summary>
        /// Un groupe est associé à ce cours. Le cours ne peut être supprimé.
        /// </summary>
        /// <returns></returns>
        public static string I_001()
        { return "Un groupe est associé à ce cours. Le cours ne peut être supprimé."; }

        /// <summary>
        /// Il existe déjà un cours ayant le code {0}.
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string I_002(string Code)
        { return $"Il existe déjà un cours ayant le code {Code}."; }

   
        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_003(string cours)
        public static string I_003(string Cours)
        { return $"Le cours {Cours} a été enregistré."; }

        /// <summary>
        /// Impossible d'enregistrer cet étudiant. Il existe déja im tudiant ayant le matricule {0}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string I_004(string matricule)
        { return $"Impossible d'enregistrer cet étudiant. Il existe déja un étudiant ayant le matricule {matricule}."; }


        /// <summary>
        /// Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string I_005()
        { return $"Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé."; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string I_006(string code)
        { return $"Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {code}."; }

        ///<summary>
        /// Le programme d'études {0} a été enregistré
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string I_007(string nomProgrammeEtude)
        { return $"Le programme d'études {nomProgrammeEtude} a été enregistré"; }

        ///<summary>
        /// Le programme d'études {0} a été supprimé
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static  string I_008(string nomProgrammeEtude)
        {  return $"Le programme d'études {nomProgrammeEtude} a été supprimer"; }


        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_009(string cours)
        public static string I_009(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }

        /// <summary>
        /// L'étudiant {0} a été enregistré.
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string I_010(string Etudiant)
        { return $"L'étudiant {Etudiant} a été enregistré."; }

        /// <summary>
        /// Impossible de retirer le programme d'étude {0} des programmes suivis par l'étudiant."
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string I_011(string Programme)
        { return $"Impossible de retirer le programme d'étude {Programme} des programmes suivis par l'étudiant."; }
        /// <summary>
        /// l'étudiant ne peut être supprimé
        /// </summary>
        /// <returns></returns>
        public static string I_014()
        { return "l'étudiant ne peut être supprimé"; }

        /// <summary>
        /// Le programme d'étude {0} a été retiré de la liste des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string I_016(string Programme)
        { return $"Le programme d'étude {Programme} a été retiré de la liste des programmes suivis par l'étudiant."; }

        //Comptes/Connexion
        /// <summary>
        /// Votre tentative de connexion a échoué. Réessayez.
        /// </summary>
        /// <returns></returns>
        public static string I_017()
        { return $"Votre tentative de connexion a échoué. Réessayez."; }

        /// <summary>
        /// Mot de passe modifié
        /// </summary>
        /// <returns></returns>
        public static string I_018()
        { return $"Mot de passe modifié"; }

        /// <summary>
        /// Votre mot de passe a été envoyé à votre adresse courriel.
        /// </summary>
        /// <returns></returns>
        public static string I_019()
        { return $"Votre mot de passe a été envoyé à votre adresse courriel."; }

        /// <summary>
        /// Un compte existe déjà pour cet étudiant.
        /// </summary>
        /// <returns></returns>
        public static string I_025()
        { return $"Un compte existe déjà pour cet étudiant."; }

        /// <summary>
        /// Le compte a été créé. Vous pouvez maintenant vous connecter.
        /// </summary>
        /// <returns></returns>
        public static string I_026()
        { return $"Le compte a été créé. Vous pouvez maintenant vous connecter."; }

        /// <summary>
        /// Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques.
        /// </summary>
        /// <returns></returns>
        public static string I_027()
        { return $"Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques."; }

        /// <summary>
        /// L'étudiant {0} a été supprimé.
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string I_028(string Etudiant)
        { return $"L'étudiant {Etudiant} a été supprimé."; }

        /// <summary>
        /// L’horaire d’inscription au SACHEM a été mis à jour.
        /// </summary>
        /// <returns></returns>
        public static string I_030()
        { return $"L’horaire d’inscription au SACHEM a été mis à jour."; }

        /// <summary>
        /// Le courriel a été mis à jour
        /// </summary>
        /// <returns></returns>
        public static string I_032()
        { return $"Le courriel a été mis à jour."; }

        /// <summary>
        /// Erreur lors du transfert de fichier.
        /// </summary>
        /// <returns></returns>
        public static string I_034(string Fichier)
        { return $"Erreur lors du transfert du fichier {Fichier}."; }

        /// <summary>
        /// Un fichier {0} de même nom est déjà présent sur le serveur.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string I_035(string Fichier)
        { return $"Un fichier {Fichier} de même nom est déjà présent sur le serveur."; }

        /// <summary>
        /// "Tous les critères de la recherche doivent être précisés."
        /// </summary>
        public static string I_039()
        { return "Tous les critères de la recherche doivent être précisés."; }

        #endregion

        #region MessageContexte
        //Contexte de comptes

        ///<summary>
        /// Le mot de passe et la confirmation du mot de passe doivent être identiques.
        /// </summary>
        /// <returns></returns>
        public const string C_001 = "Le mot de passe et la confirmation du mot de passe doivent être identiques.";

        ///<summary>
        /// L'ancien mot de passe est invalide.
        /// </summary>
        /// <returns></returns>
        public const string C_002 = "L'ancien mot de passe est invalide.";

        ///<summary>
        /// "Aucun usager associé à cette adresse courriel.
        /// </summary>
        /// <returns></returns>
        public const string C_003 = "Aucun usager associé à cette adresse courriel.";

        ///<summary>
        /// Cet usager n'existe pas ou le mot de passe est invalide.
        /// </summary>
        /// <returns></returns>
        public const string C_004 = "Cet usager n'existe pas ou le mot de passe est invalide.";

        ///<summary>
        /// La date de début doit être antérieure à la date de fin
        /// </summary>
        /// <returns></returns>
        public const string C_005 = "La date de début doit être antérieure à la date de fin.";

        ///<summary>
        /// Les dates de début et de fin doivent faire partie de la session sélectionnée.
        /// </summary>
        /// <returns></returns>
        public const string C_006 = "Les dates de début et de fin doivent faire partie de la session sélectionnée.";

        #endregion

        #region MessageUnitaire

        /// <summary>
        /// Requis
        /// </summary>
        /// <returns></returns>
        public const string U_001 = "Requis";

        /// <summary>
        /// Format : nom@nomdomaine
        /// </summary>
        /// <returns></returns>
        public const string U_003 = "Longueur requise : 8 caractères.";

        /// <summary>
        /// Longueur requise: 7 caractères.
        /// </summary>
        public const string U_004 = "Longueur requise: 7 caractères";

        /// <summary>
        /// Longueur requise: 6 caractères. (MDP)
        /// </summary>
        public const string U_005 = "Longueur requise: 6 caractères";

        /// <summary>
        /// Format : AAAA
        /// </summary>
        /// <returns></returns>
        public const string U_006 = "Format : AAAA";

        /// <summary>
        /// Format: AAAA/MM/JJ
        /// </summary>
        public const string U_007 = "Format: AAAA/MM/JJ";

        /// <summary>
        /// Format: nom@domaine.com
        /// </summary>
        /// <param name="Code"></param>
        public const string U_008 = "Format: nom@nomdomaine.com";

        ///<summary>
        /// Format : (999) 999-9999
        /// </summary>
        /// <returns></returns>
        public const string U_009 = "Format : (999) 999-9999";

        ///<summary>
        /// Format : HH:MM
        /// </summary>
        /// <returns></returns>
        public const string U_010 = "Format : HH:MM";

        /// <summary>
        /// "La date indiquée doit être entre l'année 1967 et celle en cours"
        /// </summary>
        /// <returns></returns>
        public const string U_012 = "La date indiquée doit être entre l'année 1967 et celle en cours";

        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} ?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        public static string Q_001(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours} ?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études {0} ?
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>private string Q_002(string NomProgrammeEtude)
        public static  string Q_002(string nomProgrammeEtude)
        { return $"Voulez-vous vraiment supprimer le programme d'études {nomProgrammeEtude} ?"; }

        public static string Q_011(string Etudiant)
        { return $"Voulez-vous vraiment supprimer le cours {Etudiant} ?"; }

        /// <summary>
        /// Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?
        /// </summary>
        /// <returns></returns>
        public static string Q_014()
        { return $"Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le collège {0}?
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>private string Q_015(string College)
        public static string Q_015(string College)
        { return $"Voulez-vous vraiment supprimer le collège {College} ?"; }

        #endregion
    }
}