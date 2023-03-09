namespace GymWeb.Models {
    public class ExercicioTreino {
        public int Id { get; set; }
        public int ExercicioId { get; set; }
        public int TreinoId { get; set; }
        public virtual Exercicio Exercicio { get; set; }
        public virtual Treino Treino  { get; set; }
    }
}
