using System;
using PKW.ControlCenter.Data;
using PKW.ControlCenter.Voting;

namespace PKW.ControlCenter
{
    public class DataInit
    {
        public static void Load(IDataRepository repository)
        {
            for (int i = 1; i <= 3; i++)
            {
                repository.Add(new ConstituencyModel() {Id = i, Name = String.Format("Okręg wyborczy numer {0}", i)});
            }

            repository.Add(new CandidatesModel() {Id = 1, Name = "Adam Kowalski", ConstituencyId = 1});
            repository.Add(new CandidatesModel() {Id = 2, Name = "Jan Nowak", ConstituencyId = 1});
            repository.Add(new CandidatesModel() {Id = 3, Name = "Pan Kleks", ConstituencyId = 2});
            repository.Add(new CandidatesModel() {Id = 4, Name = "Chuck Norris", ConstituencyId = 2});
            repository.Add(new CandidatesModel() {Id = 5, Name = "Krzysztof Kononowicz", ConstituencyId = 2});
            repository.Add(new CandidatesModel() {Id = 6, Name = "Bruce Lee", ConstituencyId = 2});
            repository.Add(new CandidatesModel() {Id = 7, Name = "Grzegorz Kowalski", ConstituencyId = 2});
            repository.Add(new CandidatesModel() {Id = 8, Name = "Jerzy Nowak", ConstituencyId = 3});
            repository.Add(new CandidatesModel() {Id = 9, Name = "Mr T", ConstituencyId = 3});
            repository.Add(new CandidatesModel() {Id = 10, Name = "Marlon Brando", ConstituencyId = 3});
            repository.Add(new CandidatesModel() {Id = 11, Name = "Anna Zielona", ConstituencyId = 3});
            repository.Add(new CandidatesModel() {Id = 12, Name = "John Travolta", ConstituencyId = 3});            
        }
    }
}