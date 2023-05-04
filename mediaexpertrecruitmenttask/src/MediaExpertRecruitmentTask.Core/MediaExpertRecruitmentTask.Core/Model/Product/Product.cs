#region using

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MediaExpertRecruitmentTask.Core.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

#endregion

namespace MediaExpertRecruitmentTask.Core.Model.Product;

[Table(nameof(Product), Schema = DatabaseContextSettings.DefaultSchema)]
[Index(nameof(Code), IsUnique = true)]
[Index(nameof(Name))]
public class Product
{
    #region Guid Id

    private Guid _id;

    /// <summary>
    ///     Guid Id identyfikator klucz główny
    /// </summary>
    [Key]
    [JsonProperty(nameof(Id), Order = 0)]
    [JsonPropertyName(nameof(Id))]
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid Id
    {
        get => _id;
        set
        {
            if (!value.Equals(_id))
            {
                _id = value;
            }
        }
    }

    #endregion

    #region Code

    private string _code = string.Empty;

    /// <summary>
    ///     Pole Code, string
    /// </summary>
    [JsonProperty(nameof(Code))]
    [JsonPropertyName(nameof(Code))]
    [StringLength(32)]
    [MinLength(0)]
    [MaxLength(32)]
    [Required(AllowEmptyStrings = false)]
    [Column(nameof(Code))]
    public string Code
    {
        get => _code;
        set
        {
            if (!value.Equals(_code))
            {
                _code = value;
            }
        }
    }

    #endregion

    #region Name

    private string _name = string.Empty;

    /// <summary>
    ///     Pole Name, string
    /// </summary>
    [JsonProperty(nameof(Name))]
    [JsonPropertyName(nameof(Name))]
    [StringLength(256)]
    [MinLength(0)]
    [MaxLength(256)]
    [Required(AllowEmptyStrings = true)]
    [Column(nameof(Name))]
    public string Name
    {
        get => _name;
        set
        {
            if (!value.Equals(_name))
            {
                _name = value;
            }
        }
    }

    #endregion

    #region Price

    private decimal _price;

    [JsonProperty(nameof(Price))]
    [JsonPropertyName(nameof(Price))]
    [Required]
    [Range(double.MinValue, double.MaxValue)]
    public decimal Price
    {
        get => _price;
        set
        {
            if (!value.Equals(_price))
            {
                _price = value;
            }
        }
    }

    #endregion
}