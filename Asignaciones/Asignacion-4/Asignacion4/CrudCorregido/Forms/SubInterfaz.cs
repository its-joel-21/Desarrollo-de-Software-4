using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrudWindowsForms.Data;
using CrudWindowsForms.Models;

namespace CrudWindowsForms.Forms
{
    public partial class SubInterfaz : Form
    {
        private readonly CRUD db;
        private readonly int? id;

        public SubInterfaz() : this(null) { }

        public SubInterfaz(int? id)
        {
            InitializeComponent();
            this.id = id;
            db = LicenseManager.UsageMode == LicenseUsageMode.Designtime ? null : new CRUD();
            Text = id.HasValue ? "Editar persona" : "Nueva persona";
        }

        private void FrmNuevo_Load(object sender, EventArgs e)
        {
            if (!id.HasValue || db == null) return;
            try
            {
                People person = db.GetById(id.Value);
                if (person == null)
                {
                    MessageBox.Show("El registro ya no existe. Actualice la lista.");
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                txtName.Text = person.Name;
                txtEdad.Text = person.Age.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el registro. Compruebe la conexión con SQL Server.\r\n\r\nDetalle: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private bool ValidateInput(out string name, out int age)
        {
            name = txtName.Text.Trim();
            age = 0;
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            {
                MessageBox.Show("Escriba un nombre de 1 a 100 caracteres.");
                txtName.Focus();
                return false;
            }
            if (!int.TryParse(txtEdad.Text.Trim(), out age) || age < 0 || age > 120)
            {
                MessageBox.Show("Escriba una edad entera entre 0 y 120.");
                txtEdad.Focus();
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string name;
            int age;
            if (!ValidateInput(out name, out age)) return;
            try
            {
                int affected = id.HasValue ? db.Update(id.Value, name, age) : db.Add(name, age);
                if (affected != 1)
                {
                    MessageBox.Show("SQL Server no confirmó el guardado. Actualice la lista y vuelva a intentarlo.");
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar. Compruebe que la base CrudWindowsForms exista y que App.config apunte a la instancia correcta.\r\n\r\nDetalle: " + ex.Message,
                    "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    private void txtName_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
