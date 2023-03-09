namespace GymWeb.Models.ViewModels {
    public class CreateTreinoViewModel {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Dias { get; set; }
        public int? ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
        public List<CheckBoxExercicioViewModel> checkBoxExercicios { get; set; }

    }
}
