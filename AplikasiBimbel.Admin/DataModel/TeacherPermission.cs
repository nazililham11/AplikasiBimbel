namespace AplikasiBimbel.Admin
{
    public enum TeacherPermission
    {
        /// <summary>
        /// Super Admin Permission have Full Control of Application
        /// </summary>
        Super_Admin = 0,

        /// <summary>
        /// Admin Permission Can Add New Teacher But Can't Set the Permission
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Teacher Permission 
        /// </summary>
        Teacher = 2
    }
}
