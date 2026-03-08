namespace oop_task_2_polymorphism;

class Program
{
    static void Main(string[] args)
    {
        var manager = new GradebookManager();

        manager.RegisterStrategy("MATH101", new StandardGrading());
        manager.RegisterStrategy("CS410", new PracticalGrading());
        manager.RegisterStrategy("ENG122", new ExamOnlyGrading());

        var assignments = new List<int> { 85, 90, 95 };
            
        Console.WriteLine($"MATH101 Score: {manager.GetStudentScore("MATH101", assignments, 80)}");
        Console.WriteLine($"CS410 Score: {manager.GetStudentScore("CS410", assignments, 80)}");
        Console.WriteLine($"ENG122 Score: {manager.GetStudentScore("ENG122", assignments, 80)}");
    }
}

public interface IGradeStrategy
{
    string StrategyName { get; }
    double CalculateFinalGrade(List<int> assignments, int examScore);
}

public class StandardGrading : IGradeStrategy
{
    public string StrategyName => "Standard Grading";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        double average = assignments.Count > 0 ? assignments.Average() : 0;
        return (average * 0.4) + (examScore * 0.6);
    }
}

public class PracticalGrading : IGradeStrategy
{
    public string StrategyName => "Practical Grading";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        double average = assignments.Count > 0 ? assignments.Average() : 0;
        return (average * 0.8) + (examScore * 0.2);
    }
}

public class ExamOnlyGrading : IGradeStrategy
{
    public string StrategyName => "Exam Only Grading";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        return examScore;
    }
}

public class GradebookManager
{
    private Dictionary<string, IGradeStrategy> _strategies = new Dictionary<string, IGradeStrategy>();

    public void RegisterStrategy(string courseCode, IGradeStrategy strategy)
    {
        _strategies[courseCode] = strategy;
    }

    public double GetStudentScore(string courseCode, List<int> grades, int exam)
    {
        if (!_strategies.ContainsKey(courseCode))
        {
            throw new KeyNotFoundException("Course code not found.");
        }

        if (exam < 0 || exam > 100)
        {
            throw new ArgumentException("Exam score must be between 0 and 100.");
        }

        foreach (var grade in grades)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentException("Assignment grades must be between 0 and 100.");
            }
        }

        return _strategies[courseCode].CalculateFinalGrade(grades, exam);
    }
}