using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityBelt.Models
{
  // CocktailIngredientModel myDeserializedClass = JsonSerializer.Deserialize<CocktailIngredientModel>(myJsonResponse); 
  public class CocktailIngredientModel
  {
    public List<Ingredient> ingredients { get; set; }
  }
  public class Ingredient
  {
    public string idIngredient { get; set; }
    public string strIngredient { get; set; }
    public string strDescription { get; set; }
    public string strType { get; set; }
    public string strAlcohol { get; set; }
    public string strABV { get; set; }
  }

}
