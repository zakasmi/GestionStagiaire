using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;

using System.Collections;

//using Stagiaire;



namespace Stagiaire
{
    public partial class Stagiaire : Form
    {
       


        SqlConnection cnx = new SqlConnection("Data Source=DELL ;Initial Catalog=Stagiaire ;Integrated Security=True;Pooling=False");
        SqlDataReader dr;
        int pos = 0;


//Stagiaire ------------------------------------------------------------------------------------------------------
        public Stagiaire()
        {

         Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
         Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
           
            dataGridView1.Columns.Add("identifiant","Identifiant");
            dataGridView1.Columns.Add("nom","Nom");
            dataGridView1.Columns.Add("prenom","Prenom");
            dataGridView1.Columns.Add("date de naissance","Date de Naissance");
            dataGridView1.Columns.Add("groupe", "Groupe");
            dataGridView1.Columns.Add("moyenne","Moyenne");
         

        }

       
//Lodad -----------------------------------------------------------------------------------------------------------------------
   
        private void Stagiaire_Load(object sender, EventArgs e)
        {
          //  convert(varchar(10),daten,126)
            dataGridView1.Rows.Clear();
            SqlCommand cmd = new SqlCommand("select idstg,nom,prenom,daten,groupe,moyenne from Stagiaire1", cnx);

            cnx.Open();

            dr = cmd.ExecuteReader();

            while(dr.Read()){

                dataGridView1.Rows.Add(dr[0],dr[1],dr[2],dr[3],dr[4],dr[5]);
            
            }

            cnx.Close();
            //initialiser les champs par . premiere ligne de datagridview1
            remplirchamp(0);
         
           // dataGridView1.Rows[0].Cells[3].Value
           // MessageBox.Show("" + dataGridView1.Rows[0].Cells[3].Value);
            dateTimePicke1.Value = DateTime.Parse("" + dataGridView1.Rows[0].Cells[3].Value);
            

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
//
        

//remplire Datagrid . dans ligne X ---------------------------------------------------------------------------- 
        public void remplirchamp(int x) {

            try
            {
                identifiant.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[x].Cells[5].Value.ToString();
                dateTimePicke1.Value = DateTime.Parse("" + dataGridView1.Rows[x].Cells[3].Value);
            }
            catch (Exception e1) {
                MessageBox.Show(" "+e1.Message);
            
            
            }
        }

// changer le contenue des controls ----------------------------------------------------------------------------------
       
        //La fonction bouge
        public void bouge(int i) {
            if (i < 0) { pos = 0; bouge(pos); }
            else if (i > (dataGridView1.Rows.Count - 1)) { pos = dataGridView1.Rows.Count - 1; bouge(pos); }
            else { remplirchamp(i); if (i == 0)MessageBox.Show("Debut De liste", "Debut"); else if (i == dataGridView1.Rows.Count - 1)MessageBox.Show("Fin De liste", "Fin"); }
        }
        // les buttons
        private void button1_Click(object sender, EventArgs e)
        {
            pos = 0;
            bouge(pos);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pos=dataGridView1.Rows.Count-1;
            bouge(pos);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pos = pos - 1;
            bouge(pos);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pos = pos + 1;
            bouge(pos);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pos = 0;
            LesFonctions.vider(this);
        }
//Notes ------------------------------------------------------------------------------------------------------------

        private void button9_Click(object sender, EventArgs e)
        {
            if (LesFonctions.verifiersivide(this) == true)
            {
                LesFonctions.IDstagiaire = identifiant.Text;
                LesFonctions.nom = textBox2.Text;
                LesFonctions.prenom = textBox3.Text;

                Notes notes = new Notes();
                notes.Show();
            

                this.Hide();
            }
            else MessageBox.Show("s'il vous plaît remplir les champs ,est ressayer","Notes",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void label8_Click(object sender, EventArgs e)
        {  
            Matiere matiere = new Matiere();
            matiere.Show();
            this.Hide();
        }


//Ajouter ---------------------------------------------------------------------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                
                identifiant.Text = "Auto";
               

                if (LesFonctions.verifiersivide(this) == true)
                {
                    int x;
                    SqlCommand cmd = new SqlCommand("insert into Stagiaire1 (nom,prenom,daten,groupe,moyenne) values ('"+textBox2.Text+"','"+textBox3.Text+"','"+dateTimePicke1.Value.ToString()+"','"+textBox4.Text+"',"+textBox5.Text+")", cnx);
                    cnx.Open();
                    x = cmd.ExecuteNonQuery();
                    cnx.Close();
                    Stagiaire_Load(sender, e);

                    pos = dataGridView1.Rows.Count - 1;
                    remplirchamp(pos);
                    MessageBox.Show("" + x + "Ligne affectée ,  la mise à jour a été faite", "Ajouter");
                }
                else { MessageBox.Show("s'il vous plaît remplir les champs !!", "Ajouter"); }
            }
            catch (Exception e2)
            {

                MessageBox.Show(""+ e2.Message);
                remplirchamp(pos);
            }
            finally {

                cnx.Close();
                
               
            
            }
        }

// Modifier ---------------------------------------------------------------------------------------------------------------------------  
        private void button6_Click(object sender, EventArgs e)
        {
            int x;
            bool stat=true;
            stat=LesFonctions.verifiersivide(this);

            
      
            try{
                if (stat == true)
                {

                    SqlCommand cmd = new SqlCommand("update Stagiaire1 set nom= '" + textBox2.Text + "' ,daten = '" + dateTimePicke1.Value.ToString() + "' ,prenom='" + textBox3.Text + "',groupe='" + textBox4.Text + "' , moyenne =" + textBox5.Text + " where idstg=" + identifiant.Text + "", cnx);
                    cnx.Open();
                                                                                                                //Convert.ToDecimal(textBox5.Text)                 
                    x = cmd.ExecuteNonQuery();


                    cnx.Close();
                    Stagiaire_Load(sender, e);
                    remplirchamp(pos);
                    MessageBox.Show("" + x + " affectée ,  la mise à jour a été faite ", "Update");
                }
                else { MessageBox.Show("s'il vous plaît remplir les champs !!", "Ajouter"); }

            }
            catch(Exception e3){


            MessageBox.Show(""+e3,"Error");
            
            }
            finally{

                cnx.Close();
            }
        }

        private void identifiant_TextChanged(object sender, EventArgs e)
        {

           // MessageBox.Show("Impossible De changer L ID ","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }





// Supprimer  -------------------------------------------------------------------------------------------------------------

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            int x;
            bool stat = true;
            stat = LesFonctions.verifiersivide(this);


       

            try
            {
                if (stat == true)
                {
                  
                    SqlCommand cmd = new SqlCommand("delete from  Stagiaire1  where idstg="+identifiant.Text+"", cnx);
                    cnx.Open();
                    //Convert.ToDecimal(textBox5.Text)                 
                    x = cmd.ExecuteNonQuery();


                    cnx.Close();
                    Stagiaire_Load(sender, e);
                    pos = pos - 1;
                    remplirchamp(pos);

                    MessageBox.Show("" + x + " Ligne a été supprimer ,  la mise à jour a été faite ", "Supprimer");
                }
                else { MessageBox.Show("s'il vous plaît spessifier un  !!", "Ajouter"); }

            }
            catch (Exception e3)
            {


                MessageBox.Show("" + e3, "Error");

            }
            finally
            {

                cnx.Close();

          


            }
        
        
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


           remplirchamp(e.RowIndex);
           dataGridView1.ClearSelection();

        }

        







    }
}
