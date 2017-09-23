using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stagiaire
{
    public partial class Matiere : Form
    {
        SqlConnection cnx = new SqlConnection("Data Source = Dell; Initial Catalog= Stagiaire ; Integrated Security = true; Pooling =false ");
        SqlDataReader dr;
        int pos = 0;

        public Matiere()
        {
            // si datagrid est vide on peut pas utiliser les button (supprimer , modifier , +1 ,-1,fin ,debut)
            //mais on peut utiliser les button (vider et ajouter)


            InitializeComponent();
            textBox1.Enabled = false;
            dataGridView1.Columns.Add("identifiant", "Identifiant");
            dataGridView1.Columns.Add("libelle", "Libelle");
            dataGridView1.Columns.Add("coefficient", "Coefficient");
        }
        //Matiere_ Load ------------------------------------------------------------------------------------------------
        private void Matiere_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            try
            {
                SqlCommand cmd = new SqlCommand("select *from Matiere1", cnx);
                cnx.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2]);
                }
                if (dataGridView1.Rows.Count >= 1)
                {
                    //remplire less texte Box A partir de la promiere ligne de datagrid
                    remplire(0);
                    if (dataGridView1.Rows.Count > 0) {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                    }

                }
                cnx.Close();
            }
            catch (Exception e5)
            {
                MessageBox.Show("" + e5.Message);
            }
            finally
            {
                cnx.Close();
            }

        }

        // Remplir ------------------------------------------------------------------------------------------------------------------
        public void remplire(int x)
        {

            try
            {
                textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
            }
            catch (Exception e6)
            {
                MessageBox.Show("" + e6.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Vider ------------------------------------------------------------------------------------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            LesFonctions.vider(this);
        }
        //FormClosing -------------------------------------------------------------------------------------------------------------------------------------
        private void Matiere_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stagiaire stg = new Stagiaire();
            stg.Show();
        }
        //Label Liste Stagaires -----------------------------------------------------------------------------------------------
        private void label1_Click(object sender, EventArgs e)
        {
            Stagiaire stg = new Stagiaire();
            this.Hide();
            stg.Show();


        }
        // cellcontent click datagridview1 -----------------------------------------------------------------------------------------------
    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            remplire(e.RowIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pos = 0;
            remplire(pos);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pos = dataGridView1.Rows.Count - 1;
            remplire(pos);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pos == 0) MessageBox.Show("Debut De Liste", "Impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { pos = pos - 1;
            remplire(pos);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pos == dataGridView1.Rows.Count-1) MessageBox.Show("Fin De Liste", "Impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { pos = pos + 1;
            remplire(pos);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            try{
                SqlCommand cmd = new SqlCommand("insert into Matiere1 (libelle,coeff) values('" + textBox2.Text + "'," + textBox3.Text + ")", cnx);
            int x;
        cnx.Open();

        x=cmd.ExecuteNonQuery();

        cnx.Close();
        
    MessageBox.Show(""+x+" Ligne(s) affectée(s)","Ajouter");
                    dataGridView1.Rows.Clear();
                    Matiere_Load(sender,e);

            }
            catch(Exception e7){
            
           MessageBox.Show(""+e7.Message); 
            
            }
           finally{
            
            cnx.Close();
            
            }

        }
//Supprimer -------------------------------------------------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //verifier si lid est vide ou non . 
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                SqlCommand cmd = new SqlCommand("delete from Matiere1 where idmat=" + textBox1.Text + "", cnx);
                int x;
                cnx.Open();

                x = cmd.ExecuteNonQuery();

                cnx.Close();

                MessageBox.Show("" + x + " Ligne(s) Affectée(s)", "Supprimer");
                dataGridView1.Rows.Clear();
                Matiere_Load(sender, e);
                } else MessageBox.Show("s'il vous plaît remplir les champs necessaires ,est ressayer", "Supprimer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
            catch(Exception e8) {
                MessageBox.Show(""+e8.Message);
            }
            finally{

                //en cas d'exception en doit fermer la connexion
                cnx.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // condition . true n'est pas vide . false= vide 
                if(LesFonctions.verifiersivide(this)){
                
                SqlCommand cmd = new SqlCommand("update Matiere1 set libelle ='" + textBox2.Text + "' ,coeff=" + textBox3.Text + "", cnx);
                int x;
                cnx.Open();

                x = cmd.ExecuteNonQuery();

                cnx.Close();



                MessageBox.Show("" + x + " Ligne(s) affectée(s)", "Modifier");
                }
                else MessageBox.Show("s'il vous plaît remplir les champs necessaires ,est ressayer", "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            
            }
            catch(Exception e9)
            {
                MessageBox.Show("" + e9.Message);
            }
            finally 
            {
                cnx.Close();
            }

        
        
        }


    }
}
