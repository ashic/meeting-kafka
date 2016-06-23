
import java.util

import kafka.consumer.ConsumerConfig
import kafka.producer.KeyedMessage
import kafka.serializer.StringDecoder
import org.apache.kafka.clients.producer.{KafkaProducer, ProducerConfig, ProducerRecord}
import org.apache.spark.streaming.{Seconds, StreamingContext}
import org.apache.spark.{SparkConf, SparkContext}
import org.apache.spark.streaming.kafka._

import scala.collection.JavaConversions._
import scala.collection.mutable

object DemoApp extends App {

  val threads = 2
  val conf = new SparkConf().setMaster(s"local[$threads]").setAppName("demo")
  val ssc = new StreamingContext(conf, Seconds(5))

  val brokers = "docker:32771"

  val kafkaParams = Map[String, String](
    "metadata.broker.list" -> brokers,
    "zookeeper.connect" -> "docker:2181",
    "group.id" -> "sparkdemo3"
  )
  val messages = KafkaUtils.createDirectStream[String, String, StringDecoder, StringDecoder](
    ssc, kafkaParams, Set("demo1"))

  messages.map(x => ((x._2.substring(0, 2)), 1)).reduceByKey(_ + _)
    .foreachRDD {
      rdd => {
        rdd.foreachPartition {
          partition => {
            val props = new util.HashMap[String, AnyRef]()
            props.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, brokers)
            props.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG,
              "org.apache.kafka.common.serialization.StringSerializer")
            props.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG,
              "org.apache.kafka.common.serialization.StringSerializer")
            val producer = new KafkaProducer[String, String](props)


            partition.foreach {
              case (k, v) => {
                println(s"key: $k, value: $v")
                val msg = new ProducerRecord[String, String]("demo2", k, v.toString)
                producer.send(msg)
              }
            }

          }
        }
      }
    }

  ssc.start()

  ssc.awaitTermination()
}
