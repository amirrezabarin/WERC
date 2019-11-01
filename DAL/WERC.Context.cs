﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using Model;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WERCEntities : DbContext
    {
        public WERCEntities()
            : base("name=WERCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<DietType> DietTypes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeDetail> GradeDetails { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<ParticipantRule> ParticipantRules { get; set; }
        public virtual DbSet<PaymentRule> PaymentRules { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Reference> References { get; set; }
        public virtual DbSet<RefrenceWord> RefrenceWords { get; set; }
        public virtual DbSet<SafetyItem> SafetyItems { get; set; }
        public virtual DbSet<SafetyItemDetail> SafetyItemDetails { get; set; }
        public virtual DbSet<SiteInfo> SiteInfoes { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<SurveyResult> SurveyResults { get; set; }
        public virtual DbSet<SurveyResultDetail> SurveyResultDetails { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskGrade> TaskGrades { get; set; }
        public virtual DbSet<TaskTest> TaskTests { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamGradeDetail> TeamGradeDetails { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<TeamSafetyItem> TeamSafetyItems { get; set; }
        public virtual DbSet<TeamSafetyItemDetail> TeamSafetyItemDetails { get; set; }
        public virtual DbSet<TeamSafetyItemDetailLog> TeamSafetyItemDetailLogs { get; set; }
        public virtual DbSet<TeamSafetyItemLog> TeamSafetyItemLogs { get; set; }
        public virtual DbSet<TeamTestResult> TeamTestResults { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<ExcelDictionary> ExcelDictionaries { get; set; }
        public virtual DbSet<R1> R1 { get; set; }
        public virtual DbSet<View_Test> View_Test { get; set; }
        public virtual DbSet<ViewGradeDetail> ViewGradeDetails { get; set; }
        public virtual DbSet<ViewInvoice> ViewInvoices { get; set; }
        public virtual DbSet<ViewInvoiceExtraMember> ViewInvoiceExtraMembers { get; set; }
        public virtual DbSet<ViewJudgeFullInfo> ViewJudgeFullInfoes { get; set; }
        public virtual DbSet<ViewOrder> ViewOrders { get; set; }
        public virtual DbSet<ViewPersonInRole> ViewPersonInRoles { get; set; }
        public virtual DbSet<ViewSurvey> ViewSurveys { get; set; }
        public virtual DbSet<ViewSurveyResult> ViewSurveyResults { get; set; }
        public virtual DbSet<ViewTask> ViewTasks { get; set; }
        public virtual DbSet<ViewTaskFullInfo> ViewTaskFullInfoes { get; set; }
        public virtual DbSet<ViewTaskTeam> ViewTaskTeams { get; set; }
        public virtual DbSet<ViewTeam> ViewTeams { get; set; }
        public virtual DbSet<ViewTeamFullInfo> ViewTeamFullInfoes { get; set; }
        public virtual DbSet<ViewTeamGradeDetail> ViewTeamGradeDetails { get; set; }
        public virtual DbSet<ViewTeamGradeMetaData> ViewTeamGradeMetaDatas { get; set; }
        public virtual DbSet<ViewTeamMember> ViewTeamMembers { get; set; }
        public virtual DbSet<ViewTeamSafetyItem> ViewTeamSafetyItems { get; set; }
        public virtual DbSet<ViewTeamSafetyItemLog> ViewTeamSafetyItemLogs { get; set; }
        public virtual DbSet<ViewTeamTaskTest> ViewTeamTaskTests { get; set; }
        public virtual DbSet<ViewTeamTestResult> ViewTeamTestResults { get; set; }
        public virtual DbSet<ViewTest> ViewTests { get; set; }
        public virtual DbSet<ViewUserRole> ViewUserRoles { get; set; }
        public virtual DbSet<ViewUserTask> ViewUserTasks { get; set; }
    }
}
