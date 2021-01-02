from imageai.Prediction import ImagePrediction
import os
execution_path = os.getcwd()

prediction = ImagePrediction()
prediction.setModelTypeAsSqueezeNet()
prediction.setModelPath(os.path.join(execution_path, "squeezenet_weights_tf_dim_ordering_tf_kernels.h5"))
prediction.loadModel()

def predict_a(filename):
	result = {}
	predictions, probabilities = prediction.predictImage(os.path.join(execution_path, filename), result_count=3 )
	for eachPrediction, eachProbability in zip(predictions, probabilities):
		result[eachPrediction] = eachProbability

	return result
