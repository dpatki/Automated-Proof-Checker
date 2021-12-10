﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Jeorje
{
    class Program
    {
        static void Main(string[] args)
        {
            var jeorjeInput = @"
#check ND
f(a) | forall x: T1 . x |- false
1) f(a) | forall x: T1 . x premise
2) false by magic
                ";

            Console.WriteLine(AskJeorje(jeorjeInput));
        }

        public static string AskJeorje(string input)
        {
            try
            {
                var tokens = Scanner.MaximalMunchScan(input);
                Logger.AddStep("Successfully scanned input tokens");

                var proofFormat = Transformer.TransformTokens(tokens);
                Logger.AddStep($"Found proof format, performing {proofFormat.CheckType}");

                switch (proofFormat.CheckType)
                {
                    case CheckType.ND:
                        var ndFormat = proofFormat as NDFormat;
                        List<AST> ndPremises = Parser.ParseLines(ndFormat.Premises);
                        AST ndGoal = Parser.ParseLine(ndFormat.Goal);
                        Logger.AddStep("Parsed premises and goal");
                        
                        List<NDRule> ndProof = NDRulifier.RulifyLines(ndFormat.Proof);
                        Logger.AddStep("Rulify ND worked");
                        
                        Logger.AddStep(Validator.ValidateND(ndPremises, ndGoal, ndProof));
                        break;

                    default:
                        throw new Exception($"Check type {proofFormat.CheckType.ToString()} not supported yet");
                }
            }
            
            catch (Exception e)
            {
                Logger.AddError($"Exception thrown:\n{e.Message}");
            }
            
            return Logger.LogAll();
        }
    }
}
