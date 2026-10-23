using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrudWindowsForms.Data;
using CrudWindowsForms.Models;

namespace CrudWindowsForms.Forms
{
    public partial class Interface_1 : Form
    {
        private readonly CRUD db;

        public Interface_1()
        {
            InitializeComponent();
            db = LicenseManager.UsageMode == LicenseUsageMode.Designtime ? null : new CRUD();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshPeople();
        }

        private void RefreshPeople()
        {
            if (db == null) return;
            try
            {
                dgvPeople.DataSource = null;
                dgvPeople.DataSource = db.Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las personas. Comprueba que exista la base de datos y que App.config tenga el servidor correcto.\r\n\r\nDetalle: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? GetSelectedId()
        {
            if (dgvPeople.CurrentRow == null) return null;
            People person = dgvPeople.CurrentRow.DataBoundItem as People;
            return person == null ? (int?)null : person.Id;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var form = new FrmNuevo())
                if (form.ShowDialog(this) == DialogResult.OK) RefreshPeople();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedId();
            if (!id.HasValue) { MessageBox.Show("Seleccione una persona."); return; }
            using (var form = new FrmNuevo(id.Value))
                if (form.ShowDialog(this) == DialogResult.OK) RefreshPeople();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedId();
            if (!id.HasValue) { MessageBox.Show("Seleccione una persona."); return; }
            if (MessageBox.Show("¿Desea eliminar la persona seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                if (db.Delete(id.Value) != 1) MessageBox.Show("El registro ya no existe.");
                RefreshPeople();
            }
            catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefreshPeople();
        }

    private void dgvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
