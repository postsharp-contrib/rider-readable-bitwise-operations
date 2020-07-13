After you copy this as a new plugin:
1. Modify `src\rider\main\resources\META-INF\plugin.xml` with your metadata.
2. Change the `pluginIcon.svg` in the same directory.
3. Rename the solution.
4. Modify `gradle.properties`.
5. Rename stuff in the `src` folder.

   a) Choose a name.
   
   b) The folder in `dotnet` must have that name.
   
   c) The project must be that name with `.Rider.csproj` appended. Modify the solution file so that it refers to the project with its new name.
   
   d) The assembly name must be that name.
   
   e) You must put this as the plugin ID in `settings.gradle`.
6. Modify `settings.gradle`. This will be the name of the distributable plugin ZIP file.

To use:
* Run `gradlew :build` to build.
* Run `gradlew :runIde` to test in Rider.
   *(It is expected that you will see some errors. It's fine as long as it ends with BUILD SUCCESSFUL.)*
* Run `gradlew :buildPlugin` to create a distributable ZIP file for the plugin.