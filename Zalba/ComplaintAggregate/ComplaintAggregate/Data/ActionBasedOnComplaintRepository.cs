using ComplaintAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Data
{
    public class ActionBasedOnComplaintRepository : IActionBasedOnComplaintRepository
    {
        public static List<ActionBasedOnComplaint> ListOfComplainations { get; set; } = new List<ActionBasedOnComplaint>();

        public ActionBasedOnComplaintRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ListOfComplainations.AddRange(new List<ActionBasedOnComplaint>
            {
                new ActionBasedOnComplaint
                {
                    Radnja_na_osnovu_zalbe_ID = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                    JN_ide_u_krug_sa_novim_uslovima= false,
                    JN_ide_u_krug_sa_starim_uslovima=true,
                    JN_ne_ide_u_drugi_krug=false

                },
                new ActionBasedOnComplaint
                {
                    Radnja_na_osnovu_zalbe_ID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    JN_ide_u_krug_sa_novim_uslovima= true,
                    JN_ide_u_krug_sa_starim_uslovima=false,
                    JN_ne_ide_u_drugi_krug=true


                }
            });

        }



        public List<ActionBasedOnComplaint> GetActions()
        {
            return (from e in ListOfComplainations
                    select e).ToList();
        }

        public ActionBasedOnComplaint GetActionById(Guid Radnja_na_osnovu_zalbe_ID)
        {
            return ListOfComplainations.FirstOrDefault(e => e.Radnja_na_osnovu_zalbe_ID == Radnja_na_osnovu_zalbe_ID);

        }

        public ActionBasedOnComplaint CreateAction(ActionBasedOnComplaint actionBasedOnComplaint)
        {
            actionBasedOnComplaint.Radnja_na_osnovu_zalbe_ID = Guid.NewGuid();
            ListOfComplainations.Add(actionBasedOnComplaint);
            ActionBasedOnComplaint action = GetActionById(actionBasedOnComplaint.Radnja_na_osnovu_zalbe_ID);

            return new ActionBasedOnComplaint
            {
                Radnja_na_osnovu_zalbe_ID = action.Radnja_na_osnovu_zalbe_ID,
                JN_ide_u_krug_sa_novim_uslovima = action.JN_ide_u_krug_sa_novim_uslovima,
                JN_ide_u_krug_sa_starim_uslovima = action.JN_ide_u_krug_sa_starim_uslovima,
                JN_ne_ide_u_drugi_krug = action.JN_ne_ide_u_drugi_krug,

            };
        }

        public ActionBasedOnComplaint UpdateAction(ActionBasedOnComplaint actionBasedOnComplaint)
        {
            ActionBasedOnComplaint action = GetActionById(actionBasedOnComplaint.Radnja_na_osnovu_zalbe_ID);

            action.Radnja_na_osnovu_zalbe_ID = actionBasedOnComplaint.Radnja_na_osnovu_zalbe_ID;
            action.JN_ide_u_krug_sa_novim_uslovima = actionBasedOnComplaint.JN_ide_u_krug_sa_novim_uslovima;
            action.JN_ide_u_krug_sa_starim_uslovima = actionBasedOnComplaint.JN_ide_u_krug_sa_starim_uslovima;
            action.JN_ne_ide_u_drugi_krug = actionBasedOnComplaint.JN_ne_ide_u_drugi_krug;

            return new ActionBasedOnComplaint
            {
                Radnja_na_osnovu_zalbe_ID = action.Radnja_na_osnovu_zalbe_ID,
                JN_ide_u_krug_sa_novim_uslovima = action.JN_ide_u_krug_sa_novim_uslovima,
                JN_ide_u_krug_sa_starim_uslovima = action.JN_ide_u_krug_sa_starim_uslovima,
                JN_ne_ide_u_drugi_krug = action.JN_ne_ide_u_drugi_krug,

            };
        }

        public void DeleteAction(Guid Radnja_na_osnovu_zalbe_ID)
        {
            ListOfComplainations.Remove
                (ListOfComplainations.FirstOrDefault(e => e.Radnja_na_osnovu_zalbe_ID == Radnja_na_osnovu_zalbe_ID));

        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
