using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a single training set with inputs and the desired output.
[System.Serializable]
public class TrainingSet
{
    public double[] input; // Input values for the perceptron
    public double output;  // Expected output for the given input
}

public class Perceptron_sc : MonoBehaviour
{
    public TrainingSet[] ts; // Array of training sets to train the perceptron
    double[] weights = { 0, 0 }; // Weights associated with each input
    double bias = 0; // Bias term for the perceptron
    double totalError = 0; // Accumulated error during training

    // Computes the dot product of two vectors and adds the bias term.
    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null)
            return -1; // Return -1 if inputs are null (error condition)
        if (v1.Length != v2.Length)
            return -1; // Return -1 if input vectors have different sizes (error condition)

        double d = 0; // Accumulator for the dot product
        for (int x = 0; x < v1.Length; x++)
        {
            d += v1[x] * v2[x]; // Multiply corresponding elements and sum them
        }
        d += bias; // Add bias to the result
        return d;
    }

    // Calculates the perceptron output for the training set at index i.
    double Calcoutput(int i)
    {
        double dp = DotProductBias(weights, ts[i].input); // Compute weighted sum + bias
        return dp > 0 ? 1 : 0; // Return 1 if positive, otherwise return 0
    }

    // Calculates the perceptron output for given inputs i1 and i2.
    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 }; // Construct input array
        double dp = DotProductBias(weights, inp); // Compute weighted sum + bias
        return dp > 0 ? 1 : 0; // Return 1 if positive, otherwise return 0
    }

    // Randomly initializes the weights and bias within a range.
    void InitialiseWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f); // Random weight between -1 and 1
        }
        bias = Random.Range(-1.0f, 1.0f); // Random bias between -1 and 1
    }

    // Updates the weights and bias based on the error for a given training set.
    void UpdateWeights(int j)
    {
        double error = ts[j].output - Calcoutput(j); // Calculate error (desired - actual)
        totalError += Mathf.Abs((float)error); // Accumulate absolute error

        // Adjust weights based on error and input
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += error * ts[j].input[i];
        }
        bias += error; // Adjust bias
    }

    // Trains the perceptron using the training set over a specified number of epochs.
    void Train(int epochs)
    {
        InitialiseWeights(); // Initialize weights and bias
        for (int e = 0; e < epochs; e++)
        {
            totalError = 0; // Reset total error at the start of each epoch
            for (int t = 0; t < ts.Length; t++)
            {
                UpdateWeights(t); // Update weights for each training example
                Debug.Log("W1: " + (weights[0]) + "W2: " + (weights[1]) + "B: " + bias);
            }
            Debug.Log("TOTAL ERROR: " + totalError); // Log total error for the epoch
        }
    }

    // Entry point for the script, starts the training and tests the perceptron.
    void Start()
    {
        Train(8); // Train the perceptron for 8 epochs

        // Test the perceptron with all possible combinations of inputs
        Debug.Log("Test 0 0: " + CalcOutput(0, 0));
        Debug.Log("Test 0 1: " + CalcOutput(0, 1));
        Debug.Log("Test 1 0: " + CalcOutput(1, 0));
        Debug.Log("Test 1 1: " + CalcOutput(1, 1));
    }
}
