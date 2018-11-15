<!-- default file list -->
*Files to look at*:

* **[GridMasterDetailViewController.cs](./CS/WebSolution.Module.Web/GridMasterDetailViewController.cs) (VB: [GridMasterDetailViewController.vb](./VB/WebSolution.Module.Web/GridMasterDetailViewController.vb))**
<!-- default file list end -->
# OBSOLETE - How to support Master-Detail mode in the Web ListView


<p><strong>=====================</strong><br /><strong>This article is now obsolete. Refer to the solutions described in the <a href="https://www.devexpress.com/Support/Center/p/AS12152">ListEditors - Support showing both master and detail records in the same grid control</a>  ticket.</strong><br /><strong>=====================</strong><br />This example shows the main idea of implementing Master-Detail mode in the grid on the Web using custom grid templates. Take special note that this is not complete solution by any means, and it requires additional R&D and testing. Also, for test purposes all the actions within the detail row are disabled. <br /> I have to warn all the readers that this example is applicable only for a simple scenario when only one details collection is in the class.<br /> The controller that does all the work is quite generic, and it can be used with any class that meets the condition above.<br /> This example may not work in other complex scenarios as complete support of all common scenarios in Master-Detail mode is not a trivial task, and we are not responsible for any possible problems that may appear here.<br /> </p>

<br/>


