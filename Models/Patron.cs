using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models;

public class Patron
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<Checkout> Checkouts { get; set; }
    // public decimal? Balance
    // {
    //     get
    //     {
    //         // no current checkouts, assuming any late fees have already been paid, there would be no late fees, return 0
    //         if (Checkouts == null)
    //         {
    //             return 0;
    //         }
    //         else
    //         // return checkouts where Paid = false
    //         // sum Late Fees
    //         {
    //             return Checkouts
    //             .Where(co => co.Paid == false)
    //             .Sum(co => co.LateFee);
    //         }
    //     }
    // }

    public decimal? Balance
    {
        get
        {
            decimal? totalBalance = 0M;

            foreach (Checkout checkout in Checkouts)
            {
                if (checkout.Paid == false)
                {
                    totalBalance += checkout.LateFee;
                }
            }
            return totalBalance;
        }
    }
}