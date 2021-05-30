using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Mutants.Entities.Models
{
    public class MutantsResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public double ratio { get; set; }
    }
}
