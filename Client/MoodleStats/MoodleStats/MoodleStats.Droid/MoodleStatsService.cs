﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IMoodleStatsService")]
public interface IMoodleStatsService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/LoginMoodle", ReplyAction="http://tempuri.org/IMoodleStatsService/LoginMoodleResponse")]
    MoodleObjects.MoodleUser LoginMoodle(string username, string password);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/LoginMoodle", ReplyAction="http://tempuri.org/IMoodleStatsService/LoginMoodleResponse")]
    System.Threading.Tasks.Task<MoodleObjects.MoodleUser> LoginMoodleAsync(string username, string password);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/login", ReplyAction="http://tempuri.org/IMoodleStatsService/loginResponse")]
    MoodleObjects.MoodleUser login(string username, string password);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/login", ReplyAction="http://tempuri.org/IMoodleStatsService/loginResponse")]
    System.Threading.Tasks.Task<MoodleObjects.MoodleUser> loginAsync(string username, string password);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/Register", ReplyAction="http://tempuri.org/IMoodleStatsService/RegisterResponse")]
    void Register(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/Register", ReplyAction="http://tempuri.org/IMoodleStatsService/RegisterResponse")]
    System.Threading.Tasks.Task RegisterAsync(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserByCourse", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserByCourseResponse")]
    MoodleObjects.MoodleUser[] getUserByCourse(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserByCourse", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserByCourseResponse")]
    System.Threading.Tasks.Task<MoodleObjects.MoodleUser[]> getUserByCourseAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserByID", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserByIDResponse")]
    MoodleObjects.MoodleUser getUserByID(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserByID", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserByIDResponse")]
    System.Threading.Tasks.Task<MoodleObjects.MoodleUser> getUserByIDAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getCourse", ReplyAction="http://tempuri.org/IMoodleStatsService/getCourseResponse")]
    MoodleObjects.MoodleCourse getCourse(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getCourse", ReplyAction="http://tempuri.org/IMoodleStatsService/getCourseResponse")]
    System.Threading.Tasks.Task<MoodleObjects.MoodleCourse> getCourseAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/addLocation", ReplyAction="http://tempuri.org/IMoodleStatsService/addLocationResponse")]
    void addLocation(int userID, double x, double y);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/addLocation", ReplyAction="http://tempuri.org/IMoodleStatsService/addLocationResponse")]
    System.Threading.Tasks.Task addLocationAsync(int userID, double x, double y);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/updateLocationDB", ReplyAction="http://tempuri.org/IMoodleStatsService/updateLocationDBResponse")]
    void updateLocationDB();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/updateLocationDB", ReplyAction="http://tempuri.org/IMoodleStatsService/updateLocationDBResponse")]
    System.Threading.Tasks.Task updateLocationDBAsync();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsYear", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsYearResponse")]
    System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsYear(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsYear", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsYearResponse")]
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsYearAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsMonth", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsMonthResponse")]
    System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsMonth(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsMonth", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsMonthResponse")]
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsMonthAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsWeek", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsWeekResponse")]
    System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsWeek(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getAvgsWeek", ReplyAction="http://tempuri.org/IMoodleStatsService/getAvgsWeekResponse")]
    System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsWeekAsync(int ID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/setUserTarget", ReplyAction="http://tempuri.org/IMoodleStatsService/setUserTargetResponse")]
    void setUserTarget(int userID, int target);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/setUserTarget", ReplyAction="http://tempuri.org/IMoodleStatsService/setUserTargetResponse")]
    System.Threading.Tasks.Task setUserTargetAsync(int userID, int target);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserTarget", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserTargetResponse")]
    int getUserTarget(int userID);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMoodleStatsService/getUserTarget", ReplyAction="http://tempuri.org/IMoodleStatsService/getUserTargetResponse")]
    System.Threading.Tasks.Task<int> getUserTargetAsync(int userID);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IMoodleStatsServiceChannel : IMoodleStatsService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class MoodleStatsServiceClient : System.ServiceModel.ClientBase<IMoodleStatsService>, IMoodleStatsService
{
    
    public MoodleStatsServiceClient()
    {
    }
    
    public MoodleStatsServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MoodleStatsServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MoodleStatsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MoodleStatsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public MoodleObjects.MoodleUser LoginMoodle(string username, string password)
    {
        return base.Channel.LoginMoodle(username, password);
    }
    
    public System.Threading.Tasks.Task<MoodleObjects.MoodleUser> LoginMoodleAsync(string username, string password)
    {
        return base.Channel.LoginMoodleAsync(username, password);
    }
    
    public MoodleObjects.MoodleUser login(string username, string password)
    {
        return base.Channel.login(username, password);
    }
    
    public System.Threading.Tasks.Task<MoodleObjects.MoodleUser> loginAsync(string username, string password)
    {
        return base.Channel.loginAsync(username, password);
    }
    
    public void Register(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location)
    {
        base.Channel.Register(moodleUsername, moodlePassword, password, fName, lName, location);
    }
    
    public System.Threading.Tasks.Task RegisterAsync(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location)
    {
        return base.Channel.RegisterAsync(moodleUsername, moodlePassword, password, fName, lName, location);
    }
    
    public MoodleObjects.MoodleUser[] getUserByCourse(int ID)
    {
        return base.Channel.getUserByCourse(ID);
    }
    
    public System.Threading.Tasks.Task<MoodleObjects.MoodleUser[]> getUserByCourseAsync(int ID)
    {
        return base.Channel.getUserByCourseAsync(ID);
    }
    
    public MoodleObjects.MoodleUser getUserByID(int ID)
    {
        return base.Channel.getUserByID(ID);
    }
    
    public System.Threading.Tasks.Task<MoodleObjects.MoodleUser> getUserByIDAsync(int ID)
    {
        return base.Channel.getUserByIDAsync(ID);
    }
    
    public MoodleObjects.MoodleCourse getCourse(int ID)
    {
        return base.Channel.getCourse(ID);
    }
    
    public System.Threading.Tasks.Task<MoodleObjects.MoodleCourse> getCourseAsync(int ID)
    {
        return base.Channel.getCourseAsync(ID);
    }
    
    public void addLocation(int userID, double x, double y)
    {
        base.Channel.addLocation(userID, x, y);
    }
    
    public System.Threading.Tasks.Task addLocationAsync(int userID, double x, double y)
    {
        return base.Channel.addLocationAsync(userID, x, y);
    }
    
    public void updateLocationDB()
    {
        base.Channel.updateLocationDB();
    }
    
    public System.Threading.Tasks.Task updateLocationDBAsync()
    {
        return base.Channel.updateLocationDBAsync();
    }
    
    public System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsYear(int ID)
    {
        return base.Channel.getAvgsYear(ID);
    }
    
    public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsYearAsync(int ID)
    {
        return base.Channel.getAvgsYearAsync(ID);
    }
    
    public System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsMonth(int ID)
    {
        return base.Channel.getAvgsMonth(ID);
    }
    
    public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsMonthAsync(int ID)
    {
        return base.Channel.getAvgsMonthAsync(ID);
    }
    
    public System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]> getAvgsWeek(int ID)
    {
        return base.Channel.getAvgsWeek(ID);
    }
    
    public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<MoodleObjects.MoodleCourse, MoodleObjects.LocationAvg[]>> getAvgsWeekAsync(int ID)
    {
        return base.Channel.getAvgsWeekAsync(ID);
    }
    
    public void setUserTarget(int userID, int target)
    {
        base.Channel.setUserTarget(userID, target);
    }
    
    public System.Threading.Tasks.Task setUserTargetAsync(int userID, int target)
    {
        return base.Channel.setUserTargetAsync(userID, target);
    }
    
    public int getUserTarget(int userID)
    {
        return base.Channel.getUserTarget(userID);
    }
    
    public System.Threading.Tasks.Task<int> getUserTargetAsync(int userID)
    {
        return base.Channel.getUserTargetAsync(userID);
    }
}
