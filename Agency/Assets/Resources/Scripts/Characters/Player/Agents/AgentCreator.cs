using System;
using System.Collections.Generic;

/// <summary>
/// Maps Agent -> AgentType -> AgentController
/// </summary>
public static class AgentCreator
{
    private static Dictionary<AgentType, Type> mapping = new Dictionary<AgentType, Type>
    {
        { AgentType.Standard, typeof(StandardAgentController) }, // Default pistol w/ nades
        { AgentType.Breacher, typeof(BreacherAgentController) }, // Flashbangs + SMG
        { AgentType.Elite, typeof(EliteAgentController) },       // AR + combat roll
        { AgentType.Joker, typeof(JokerAgentController) },       // Grenades and shotgun
        { AgentType.Riot, typeof(RiotAgentController) }          // Riot (reflective?) shield + pistol
    };
    
    public static AAgentController CreateAgent(AgentType type)
    {
        return (AAgentController)Activator.CreateInstance(mapping[type]);
    }
}