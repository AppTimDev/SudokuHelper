using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SudokuHelper
{
    public partial class TreeviewForm : Form
    {
        //private MyTreeView treeView1;
        public TreeviewForm()
        {
            InitializeComponent();
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);

            this.treeView1.CheckBoxes = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;

            this.tbNode.Text = "Node";
            this.treeView1.Nodes.Add("Root");
            TreeNode node1 = this.treeView1.Nodes[0].Nodes.Add("C");
            node1.Nodes.Add("C Programming Book 1");
            node1.Nodes.Add("C Programming Book 2");

            TreeNode node2 = this.treeView1.Nodes[0].Nodes.Add("C++");
            node2.Nodes.Add("C++ Book 1");
            node2.Nodes.Add("C++ Book 2");
            node2.Nodes.Add("C++ Book 3");

            this.treeView1.Nodes[0].Nodes.Add("C#");
            this.treeView1.ExpandAll();            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(this.tbNode.Text.Trim());
            TreeNode SelectedNode = this.treeView1.SelectedNode;
            if (SelectedNode != null)
            {
                SelectedNode.Nodes.Add(node);
            }
            else
            {
                this.treeView1.Nodes.Add(node);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                this.treeView1.SelectedNode.Text = this.tbNode.Text.Trim();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                this.treeView1.SelectedNode.Remove();
            }            
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("treeView1_Click");
            
            //this.treeView1.SelectedNode = null;
        }
        private void ShowCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    Console.WriteLine(node.Text);
                }
                ShowCheckedNodes(node.Nodes);
            }
        }
        private void btnStatus_Click(object sender, EventArgs e)
        {
            Console.WriteLine(this.treeView1.SelectedNode);
            Console.WriteLine("Show checked nodes:");
            ShowCheckedNodes(this.treeView1.Nodes);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("treeView1_DoubleClick");
            var localPosition = treeView1.PointToClient(Cursor.Position);
            var hitTestInfo = treeView1.HitTest(localPosition);
            if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                return;
        }
        
        private void DeleteCheckdNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }
                else
                {
                    DeleteCheckdNodes(node.Nodes);
                }
            }
            foreach (var checkedNode in checkedNodes)
            {
                nodes.Remove(checkedNode);
            }
        }
        private void btnDeleteChecked_Click(object sender, EventArgs e)
        {
            DeleteCheckdNodes(treeView1.Nodes);
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("treeView1_MouseUp");
            //unselect item when clicking outside of tree
            this.treeView1.SelectedNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(Control.MousePosition));
            Console.WriteLine($"this.treeView1.SelectedNode: {this.treeView1.SelectedNode}");

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Console.WriteLine($"treeView1_AfterCheck: {e.Action}");
            if (e.Action != TreeViewAction.Unknown)
            {
                try
                {
                    e.Node.TreeView.BeginUpdate();
                    CheckChildNodes(e.Node, e.Node.Checked);
                }
                finally
                {
                    e.Node.TreeView.EndUpdate();
                }
            }
        }
        private void CheckChildNodes(TreeNode node, Boolean bChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = bChecked;
                if (item.Nodes.Count > 0)
                {
                    this.CheckChildNodes(item, bChecked);
                }
            }
        }
    }
}
