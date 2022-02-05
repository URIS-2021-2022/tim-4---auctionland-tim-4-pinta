using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class TypeOfComplaintRepository : ITypeOfComplaintRepository
    {
        public static List<TypeOfComplaint> ListOfComplainations { get; set; } = new List<TypeOfComplaint>();

        public TypeOfComplaintRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ListOfComplainations.AddRange(new List<TypeOfComplaint>
            {
                new TypeOfComplaint
                {
                    Tip_id = Guid.Parse("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                    Zalba_na_tok_javnog_nadmetanja= "tok javnog nadmetanja",
                    Zalba_na_odluku_o_davanju_na_koriscenje="davanje na koriscenje",
                    Zalba_na_odluku_o_davanju_na_zakup="davanje na zakup"

                },
                new TypeOfComplaint
                {
                    Tip_id = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                    Zalba_na_tok_javnog_nadmetanja= "tok javnog nadmetanja1",
                    Zalba_na_odluku_o_davanju_na_koriscenje="davanje na koriscenje1",
                    Zalba_na_odluku_o_davanju_na_zakup="davanje na zakup1"
                }
            });
        }

        public TypeOfComplaint CreateTypeOfComplaint(TypeOfComplaint typeOfComplaint)
        {
            typeOfComplaint.Tip_id = Guid.NewGuid();
            ListOfComplainations.Add(typeOfComplaint);
            TypeOfComplaint action = GetTypesOfComplaintsById(typeOfComplaint.Tip_id);

            return new TypeOfComplaint
            {
                Tip_id = action.Tip_id,
                Zalba_na_tok_javnog_nadmetanja = action.Zalba_na_tok_javnog_nadmetanja,
                Zalba_na_odluku_o_davanju_na_zakup = action.Zalba_na_odluku_o_davanju_na_zakup,
                Zalba_na_odluku_o_davanju_na_koriscenje = action.Zalba_na_odluku_o_davanju_na_koriscenje,

            };
        }

        public void DeleteTypeOfComplaint(Guid Tip_id)
        {
            ListOfComplainations.Remove
                (ListOfComplainations.FirstOrDefault(e => e.Tip_id == Tip_id));

        }

        public List<TypeOfComplaint> GetTypesOfComplaints()
        {
            return (from e in ListOfComplainations
                    select e).ToList();
        }

        public TypeOfComplaint GetTypesOfComplaintsById(Guid Tip_id)
        {
            return ListOfComplainations.FirstOrDefault(e => e.Tip_id == Tip_id);

        }

        public TypeOfComplaint UpdateTypeOfComplaint(TypeOfComplaint typeOfComplaint)
        {
            TypeOfComplaint action = GetTypesOfComplaintsById(typeOfComplaint.Tip_id);

            action.Tip_id = typeOfComplaint.Tip_id;
            action.Zalba_na_tok_javnog_nadmetanja = typeOfComplaint.Zalba_na_tok_javnog_nadmetanja;
            action.Zalba_na_odluku_o_davanju_na_zakup = typeOfComplaint.Zalba_na_odluku_o_davanju_na_zakup;
            action.Zalba_na_odluku_o_davanju_na_koriscenje = typeOfComplaint.Zalba_na_odluku_o_davanju_na_koriscenje;

            return new TypeOfComplaint
            {
                Tip_id = action.Tip_id,
                Zalba_na_tok_javnog_nadmetanja = action.Zalba_na_tok_javnog_nadmetanja,
                Zalba_na_odluku_o_davanju_na_zakup = action.Zalba_na_odluku_o_davanju_na_zakup,
                Zalba_na_odluku_o_davanju_na_koriscenje = action.Zalba_na_odluku_o_davanju_na_koriscenje,

            };
        }
    }
}
