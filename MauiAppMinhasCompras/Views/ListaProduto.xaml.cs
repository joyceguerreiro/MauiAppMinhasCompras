using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

        lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        List<Produto> tpm = await App.Db.GetAll();

        tpm.ForEach(i => lista.Add(i));
    }
    
    //Botão Adicionar
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    //Botão Buscar
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tpm = await App.Db.Search(q);

        tpm.ForEach(i => lista.Add(i));
    }

    //Botão Somar
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK"); 
    }

    //Botão Remover
    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)sender;
        var produto = (Produto)menuItem.BindingContext; 

        if (produto != null)
        {
            lista.Remove(produto); 
            lst_produtos.ItemsSource = null; 
            lst_produtos.ItemsSource = lista;
        }

    }

}