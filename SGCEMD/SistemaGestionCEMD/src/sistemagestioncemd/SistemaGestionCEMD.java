/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sistemagestioncemd;

import javafx.application.Application;
import static javafx.application.Application.launch;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.stage.Stage;

/**
 *
 * @author felipe
 */
public class SistemaGestionCEMD extends Application {
    
    //Definimos las ventanas a utilizar
    public static String screenLoginID = "Login";
    public static String screenLoginFile = "Login.fxml";
    public static String screenInicioAdministradorID = "InicioAdministrador";
    public static String screenInicioAdministradorFile = "InicioAdministrador.fxml";
    public static String screenAgregarAlumnoID = "AgregarAlumno";
    public static String screenAgregarAlumnoFile = "AgregarAlumno.fxml";
    public static String screenAgregarCentroID = "AgregarCentro";
    public static String screenAgregarCentroFile = "AgregarCentro.fxml";
    public static String screenAgregarFamiliaID = "AgregarFamilia";
    public static String screenAgregarFamiliaFile = "AgregarFamilia.fxml";
    public static String screenBuscarUsuarioID = "BuscarUsuario";
    public static String screenBuscarUsuarioFile = "BuscarUsuario.fxml";
    public static String screenEliminarUsuarioID = "EliminarUsuario";
    public static String screenEliminarUsuarioFile = "EliminarUsuario.fxml";
    public static String screenEliminarCentroID = "EliminarCentro";
    public static String screenEliminarCentroFile = "EliminarCentro.fxml";
    public static String screenModificarCentroID = "ModificarCentro";
    public static String screenModificarCentroFile = "ModificarCentro.fxml";
    public static String screenModificarUsuarioID = "ModificarUsuario";
    public static String screenModificarUsuarioFile = "ModificarUsuario.fxml";
    public static String screenMostrarCentroID = "MostrarCentro";
    public static String screenMostrarCentroFile = "MostrarCentro.fxml";
    public static String screenMostrarUsuarioID = "MostrarUsuario";
    public static String screenMostrarUsuarioFile = "MostrarUsuario.fxml";
    
    @Override
    public void start(Stage primaryStage) {
        
        ScreensController mainContainer = new ScreensController();
        mainContainer.loadScreen(SistemaGestionCEMD.screenLoginID, SistemaGestionCEMD.screenLoginFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenInicioAdministradorID, SistemaGestionCEMD.screenInicioAdministradorFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenAgregarAlumnoID, SistemaGestionCEMD.screenAgregarAlumnoFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenAgregarCentroID, SistemaGestionCEMD.screenAgregarCentroFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenAgregarFamiliaID, SistemaGestionCEMD.screenAgregarFamiliaFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenBuscarUsuarioID, SistemaGestionCEMD.screenBuscarUsuarioFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenEliminarUsuarioID, SistemaGestionCEMD.screenEliminarUsuarioFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenEliminarCentroID, SistemaGestionCEMD.screenEliminarCentroFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenModificarCentroID, SistemaGestionCEMD.screenModificarCentroFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenModificarUsuarioID, SistemaGestionCEMD.screenModificarUsuarioFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenMostrarCentroID, SistemaGestionCEMD.screenMostrarCentroFile);
        mainContainer.loadScreen(SistemaGestionCEMD.screenMostrarUsuarioID, SistemaGestionCEMD.screenMostrarUsuarioFile);
        
        mainContainer.setScreen(SistemaGestionCEMD.screenLoginID);
        
        Group root = new Group();
        root.getChildren().addAll(mainContainer);        
        Scene scene = new Scene(root);
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
}