using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stagiaire
{
    class LesFonctions
    {
           
         public static string IDstagiaire;
         public static string nom;
         public static string prenom;
         //public static string groupe;
         //public static string 
         //public static string 


// Class Static Vider Les Champs d'une Form , donner en parametr--------------------------------------------------------
        public static void vider(Form F) { 
        
            foreach(Control C in F.Controls){

                if (C is TextBox )
                {
                    C.Text =null;
                }
                if (C is ComboBox) {
                    //instancier est initialiser pour eviter les probl
                    ComboBox combo = (ComboBox)C;
                    combo.SelectedIndex = -1;
                }
                if(C is DateTimePicker){
                    //initialiser Datetimepicker par la date actuel
                    DateTimePicker date = (DateTimePicker)C;
                    C.Text = null;
                
                }
            }
            
        
        }


//Verifier--------------------------------------------------------------------------------------------
        public static bool verifiersivide(Form F) {
            bool stat = true;
        foreach(Control C in F.Controls){

            if (C is TextBox) { if (String.IsNullOrEmpty(C.Text)) { stat = false; } }
            if (C is DateTimePicker) { if (C.Text == null) { stat = false; } }
            if (C is ComboBox) { if (String.IsNullOrEmpty(C.Text)) { stat = false; } }
        }

        return stat;
        }
    }
}
